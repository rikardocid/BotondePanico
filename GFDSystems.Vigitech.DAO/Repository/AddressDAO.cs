using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAO.CustomResponse;
using GFDSystems.Vigitech.DAO.Helper;
using GFDSystems.Vigitech.DAO.Interfaces;
using GFDSystems.Vigitech.DAO.Register;
using GFDSystems.Vigitech.DAO.Tools.Extensions;
using GFDSystems.Vigitech.DAO.Tools.Utilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Repository
{
    public class AddressDAO : IAddressDAO
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomSystemLog _customSystemLog;

        public AddressDAO(ApplicationDbContext context , ICustomSystemLog customSystemLog)
        {
            _context = context;
            _customSystemLog = customSystemLog;
        }
        public IQueryable<Address> GetAll()
        {
            return _context.Set<Address>().AsNoTracking();
        }
        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.AddressId.Equals(id));
        }
        public async Task<Address> CreateAsync(Address address)
        {
            await _context.addresses.AddAsync(address);
            await SaveAllAsync(address);
            return address;
        }
        public async Task<Address> UpdateAsync(Address address)
        {
            _context.addresses.Update(address);
            await SaveAllAsync(address);
            return address;
        }
        public async Task DeleteAsync(Address address)
        {
            _context.addresses.Remove(address);
            await SaveAllAsync(address);
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _context.addresses.AnyAsync(e => e.AddressId.Equals(id));
        }
        public async Task<bool> ExistCizenAsync(int id)
        {
            return await _context.addresses.AnyAsync(e => e.AddressId.Equals(id));
        }
        public async Task<bool> SaveAllAsync(Address address)
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                //var response = new StatusResponse<string>();
                SystemLog systemLog = new SystemLog();
                systemLog.Description = ex.ToMessageAndCompleteStacktrace();
                systemLog.DateLog = DateTime.UtcNow.ToLocalTime();
                systemLog.Controller = GetType().Name;
                systemLog.Action = UtilitiesAIO.GetCallerMemberName();
                systemLog.Parameter = JsonConvert.SerializeObject(address);
                _customSystemLog.AddLog(systemLog);
                //response.StatusCode = HttpStatusCode.InternalServerError;
                //response.Message = "Error al intentar hacer el registro";
                //response.Value = null;
                return false;
            }
        }
    }
}
