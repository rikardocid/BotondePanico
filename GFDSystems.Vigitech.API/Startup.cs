using GFDSystems.Vigitech.API.Tools.Security;
using GFDSystems.Vigitech.API.Tools.Services;
using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAL.Models;
using GFDSystems.Vigitech.DAO.Auth;
using GFDSystems.Vigitech.DAO.Auth.RoleManager;
using GFDSystems.Vigitech.DAO.Interfaces;
using GFDSystems.Vigitech.DAO.MongoDB;
using GFDSystems.Vigitech.DAO.MySQL;
using GFDSystems.Vigitech.DAO.Register;
using GFDSystems.Vigitech.DAO.Repository;
using GFDSystems.Vigitech.DAO.Tools.Email;
using GFDSystems.Vigitech.DAO.Tools.Mapping;
using GFDSystems.Vigitech.DAO.Tools.Settings;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using static GFDSystems.Vigitech.API.Tools.Security.CustomRequireClaim;

namespace GFDSystems.Vigitech.API
{
    public class Startup
    {
        private readonly string GROUP_NAME = "API";
        private readonly string VERSION = "1.0Alpha";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Variables
            var settings = Configuration.GetSection("AppIdentitySettings").Get<AppIdentitySettings>();
            var mailKitOptions = Configuration.GetSection("EmailSettings").Get<MailKitOptions>();
            #endregion

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("VigitechConnection")),
                ServiceLifetime.Transient
            );
            services.AddDbContext<MySQLContext>
                (o => o.UseMySQL(Configuration.
                GetConnectionString("MySQLConection"))
             );

            services.AddIdentity<AppUser, AppRole>(cfg =>
            {
                //User Settings
                cfg.User.RequireUniqueEmail = settings.User.RequireUniqueEmail;

                //Password settings
                cfg.Password.RequireDigit = settings.Password.RequireDigit;
                cfg.Password.RequiredLength = settings.Password.RequiredLength;
                cfg.Password.RequireNonAlphanumeric = settings.Password.RequireNonAlphanumeric;
                cfg.Password.RequireUppercase = settings.Password.RequireUppercase;
                cfg.Password.RequireLowercase = settings.Password.RequireLowercase;

                //Lockout settings
                cfg.Lockout.AllowedForNewUsers = settings.Lockout.AllowedForNewUsers;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(settings.Lockout.DefaultLockoutTimeSpanInMins);
                cfg.Lockout.MaxFailedAccessAttempts = settings.Lockout.MaxFailedAccessAttempts;

                //SigIn setting
                cfg.SignIn.RequireConfirmedEmail = settings.SigIn.RequireConfirmedEmail;
                cfg.SignIn.RequireConfirmedPhoneNumber = settings.SigIn.RequireConfirmedPhoneNumber;
                cfg.SignIn.RequireConfirmedAccount = settings.SigIn.RequireConfirmedAccount;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
            //    options.Audience = Configuration["Auth0:Audience"];
            //});

            //Add policys for save end points autorization
            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Claim.DoB", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.Role);
                });
            });
            //add Scoped from layer DAO
            services.AddScoped<IRegisterRepository, RegisterRepository>();
            services.AddScoped<ICustomSystemLog, CustomSystemLog>();
            services.AddScoped<IStateDAO, StateDAO>();
            services.AddScoped<IEmergencyDirectoryDAO, EmergencyDirectoryDAO>();
            services.AddScoped<ITypeIncidenceDAO, TypeIncidenceDAO>();
            services.AddScoped<ITypeEmergencyDAO, TypeEmergencyDAO>();
            services.AddScoped<IEmergencyAlertDAO, EmergencyAlertDAO>();
            services.AddScoped<IAddressDAO, AddressDAO>();
            services.AddScoped<ICitizenDAO, CitizenDAO>();
            services.AddScoped<IEmergencyContactDAO, EmergencyContactDAO>();
            services.AddScoped<IMedicalRecordDAO, MedicalRecordDAO>();
            services.AddScoped<ISuburbDAO, SuburbDAO>();
            services.AddScoped<ITownDAO, TownDAO>();
            services.AddScoped<IMobileDeviceRegistrationTempDAO, MobileDeviceRegistrationTempDAO>();
            services.AddScoped<ISecurityAgentDAO, SecurityAgentDAO>();
            services.AddScoped<ISecurityAgentEmergencyDAO, SecurityAgentEmergencyDAO>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IGpsTrackerDAO, GpsTrackerDAO>();
            services.AddScoped<IUserToolkitRepository, UserToolkitRepository>();
            services.AddScoped<IRoleManagerRepository, RoleManagerRepository>();
            services.AddScoped<IProfileDAO, ProfileDAO>();
            services.AddScoped<IVehicleDeviceDAO, VehicleDeviceDAO>();
            //ENDasdf

            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

            //Inject Settings
            services.Configure<AppIdentitySettings>(Configuration.GetSection("AppIdentitySettings"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            //Auto Mapper Cast Object into DAL to DAO
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }, AppDomain.CurrentDomain.GetAssemblies());

            //MailKit Use to send Email confirmation for each of citizen records
            services.AddMailKit(config => config.UseMailKit(mailKitOptions));

            //Service to create html from razor page model *cshtml 
            services.AddScoped<IViewRenderService, ViewRenderService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen();
            AddSwagger(services); 
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //Area swagger data
            app.UseSwagger(c => c.SerializeAsV2 = true);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{GROUP_NAME}/swagger.json", "My API V1");
                c.RoutePrefix = "";
            });

            //END Area swagger data

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region AddSwagger 
        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {

                opt.SwaggerDoc(GROUP_NAME, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = $"Vigitech {GROUP_NAME}",
                    Version = VERSION,
                    Description = "Service API REST Application Developed for alert management from appliaction mobile called 'VigitechMobile' and from the part web VigitechWeb both platforms they will communicate to this service",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "GFDSYSTEMS",
                        Email = "direccion@gfdsystems.com.mx",
                        Url = new Uri("http://gfdsystems.com.mx/"),
                    }
                });

                opt.AddSecurityDefinition("X-API-Key", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the APIKEY scheme",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "ApiKey",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
                opt.ExampleFilters();
                opt.OperationFilter<AddResponseHeadersFilter>();
                opt.OperationFilter<AppendAuthorizeToSummaryOperationFilter<ApiKeyAttribute>>();
                //opt.OperationFilter<SecurityRequirementsOperationFilter<ApiKeyAttribute>>();

            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        }
        #endregion
    }
}
