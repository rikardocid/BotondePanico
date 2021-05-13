using GFDSystems.Vigitech.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GFDSystems.Vigitech.DAL.Log
{
    public class CustomSystemLog : ICustomSystemLog
    {
        private readonly ApplicationDbContext _context;

        public CustomSystemLog(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddLog(SystemLog systemLog)
        {
            ResetContextState();
            _context.SystemLogs.Add(systemLog);
            int returnValue = _context.SaveChanges();
            return returnValue > 0 ? true : false;
        }

        private void ResetContextState() => _context.ChangeTracker.Entries()
                                .Where(e => e.Entity != null && e.State == EntityState.Added)
                                .ToList().ForEach(e => e.State = EntityState.Detached);
    }
}
