using GFDSystems.Vigitech.DAL.Entities.MySQL;
using Microsoft.EntityFrameworkCore;

namespace GFDSystems.Vigitech.DAL
{
    public class MySQLContext : DbContext
    {
        public DbSet<VehicleDevice> vehicleDevices { get; set; }
        public DbSet<GroupInfo> groupInfos { get; set; }
        public MySQLContext(DbContextOptions<MySQLContext>options) : base(options) {}
        
    }
}