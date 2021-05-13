
using System;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.ControlEmergencias;
using GFDSystems.Vigitech.DAL.Entities.Usuarios;
using GFDSystems.Vigitech.DAL.Enums;
using GFDSystems.Vigitech.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Type = GFDSystems.Vigitech.DAL.Entities.Type;


namespace GFDSystems.Vigitech.DAL
{

    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<ApiKeyUser> ApiKeyUsers { get; set; }
        public DbSet<Citizen> citizens { get; set; }
        public DbSet<EmergencyContact> emergencyContacts { get; set; }
        public DbSet<MedicalRecord> medicalRecords { get; set; }
        public DbSet<MobileDeviceRegistration> mobileDeviceRegistrations { get; set; }
        public DbSet<MobileDeviceRegistrationTemp> MobileDeviceRegistrationTemps { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<Town> towns { get; set; }
        public DbSet<Suburb> suburbs { get; set; }
        public DbSet<Address>addresses { get; set; }
        public DbSet<EmergencyDirectory> emergencyDirectories { get; set; }
        public DbSet<GroupType> groupTypes { get; set; }
        public DbSet<Type> types { get; set; }
        public DbSet<GroupStatus> groupStatuses { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<TypeEmergency> typeEmergencies { get; set; }
        public DbSet<TypeIncidence> typeIncidences { get; set; }
        public DbSet<EmergencyAlert> emergencyAlerts { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<SecurityAgent> securityAgents { get; set; }
        public DbSet<SecurityAgentEmergency> securityAgentEmergencies { get; set; }
        public DbSet<NotificationBase> NotificationBases { get; set; }
        public DbSet<FoliosControl> FoliosControls { get; set; }
        public DbSet<StatusDevice> statusDevices { get; set; }
        public DbSet<DeviceAssigned> deviceAssigneds { get; set; }
        public DbSet<HistoryDeviceAssigned> historyDeviceAssigneds { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            int stringMaxLength = 100;
            builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.ProviderKey).HasMaxLength(stringMaxLength));

            // We are using int here because of the change on the PK
            builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(stringMaxLength));
            builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.Name).HasMaxLength(stringMaxLength));

            builder.Entity<AppUser>()
                .HasOne<ApiKeyUser>(a => a.ApiKeyUser)
                .WithOne(u => u.User)
                .HasForeignKey<ApiKeyUser>(ak => ak.AppUserId);

            builder.Entity<AppUser>()
                .Property(x => x.IsVerified)
                .HasDefaultValue("false");

            builder.Entity<NotificationBase>()
                .Property(x => x.IsActive)
                .HasDefaultValue("true");

            builder.Entity<FoliosControl>()
                .Property(x => x.IsActive)
                .HasDefaultValue("true");

            builder.Entity<NotificationBase>()
                .Property(e => e.LevelPriority)
                .HasConversion(
                    c => c.ToString(),
                    c => (Priority) Enum.Parse(typeof(Priority), c))
                .HasMaxLength(24);
        }
    }
}