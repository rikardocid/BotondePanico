using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.API.Tools.Security
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public string[] Policies { get; set; }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                await next();

            if (!context.HttpContext.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "No se proporcionó la clave de API"
                };
                return;
            }

            using var dbContext = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            if (dbContext != null)
            {
                var apiKey = await dbContext.ApiKeyUsers
                    .Include(u => u.User)
                    .Where(x => x.Key == extractedApiKey.ToString()).SingleOrDefaultAsync();
            
                if (apiKey == null)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "Api Key no es valida"
                    };
                    return;
                }
                if (!apiKey.IsActive)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "Api Key no esta activa favor de comunicarse a soporte técnico"
                    };
                    return;
                }

                if (Policies != null)
                {
                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
                    var dataRoles = await userManager.GetRolesAsync(apiKey.User);

                    if(dataRoles.Count == 0)
                    {
                        context.Result = new ContentResult()
                        {
                            StatusCode = 401,
                            Content = "No Cuenta con permisos para accesar a la información "
                        };
                        return;
                    }

                    //var data = (from R in dbContext.Roles.ToList()
                    //            join ur in dbContext.UserRoles.Where(x => x.UserId == API_KEY.AppUserId).ToList() 
                    //            on R.Id equals ur.RoleId
                    //            where Policies.Contains(R.Name)
                    //            select R.Name
                    //            ).ToList();

                    if (!dataRoles.Any(x => Policies.Contains(x)))
                    {
                        context.Result = new ContentResult()
                        {
                            StatusCode = 401,
                            Content = "No Cuenta con los suficientes permisos para accesar a la información "
                        };
                        return;
                    }
                
                }
            }
            else
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 500,
                    Content = "Servicio fuera de linea favor de verificar"
                };
            }

            await next();
        }
    }
}
