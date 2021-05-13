using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Security;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GFDSystems.Vigitech.DAO.Tools.Utilities
{
    public class UserToolkitRepository : IUserToolkitRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomSystemLog _customSystemLog;

        public UserToolkitRepository(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            _context = context;
            _customSystemLog = customSystemLog;
        }

        public string[] GenerateUser(string TypeUser)
        {
            int consecutivo = 0;
            string folioPadLeft = string.Empty;
            var Types = (from t in _context.types.ToList()
                          join g in _context.groupTypes.Where(x => x.Name == "Usuarios").ToList()
                          on t.GroupTypeId equals g.GroupTypeID
                          select t).Where(x => x.Name == TypeUser).ToList();
            if(Types.Count == 0)
            {
                return null;
            }
            var folio = _context.FoliosControls.Where(x => x.Type == Types.FirstOrDefault().TypeId).FirstOrDefault();
            try
            {
                if (folio != null)
                {
                    using (var scope = _context.Database.BeginTransaction())
                    {

                        consecutivo = folio.Secuencial + 1;
                        folio.Secuencial = consecutivo;
                        folioPadLeft = consecutivo.ToString().PadLeft(3, '0');
                        _context.Entry(folio).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        scope.Commit();
                    }
                    return new string[] { string.Format("{0}{1}{2}", folio.Prefix, folioPadLeft, folio.Sufix), Types.FirstOrDefault().TypeId };
                }

                return null;
            }
            catch (Exception e)
            {
                SystemLog systemLog = new SystemLog();
                systemLog.Description = e.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(new { Type = TypeUser});
                _customSystemLog.AddLog(systemLog);
                return null;
            }
        }

        public string GenerateUserPaasword()
        {
            return KeyGenerator.GetUniqueKeyOriginal_BIASED(6);
        }
    }
}
