using GFDSystems.Vigitech.DAL;
using GFDSystems.Vigitech.DAL.Entities;
using GFDSystems.Vigitech.DAL.Entities.Responses;
using GFDSystems.Vigitech.DAL.Log;
using GFDSystems.Vigitech.DAO.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GFDSystems.Vigitech.DAO.Repository
{
    public class ProfileDAO : IProfileDAO
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomSystemLog _customSystemLog;

        public ProfileDAO(ApplicationDbContext context, ICustomSystemLog customSystemLog)
        {
            _context = context;
            _customSystemLog = customSystemLog;
        }
        public IQueryable<ProfileResponse> GetByIdAsync(int id)//pido el id del aspnetuser
        {
            #region Option2Query
            //List<EmergencyContact> emergencyContacts = _context.emergencyContacts
            //    .Where(w => w.CitizenId == (
            //        _context.appUsers.Join(
            //        _context.citizens, ap => ap.Id, ci => ci.AspNetUserId, (ap, ci) => new { ap, ci })
            //        .Where(w => w.ap.Id == id)
            //        .Select(s => s.ci.CitizenId).FirstOrDefault()
            //    )).ToList();
            //var query = from au in _context.appUsers
            //            join ci in _context.citizens on au.Id equals ci.AspNetUserId
            //            join ad in _context.addresses on ci.CitizenId equals ad.CitizenId
            //            join sb in _context.suburbs on ad.SuburbId equals sb.SuburbId
            //            join mr in _context.medicalRecords on ci.CitizenId equals mr.CitizenId
            //            where au.IsActive == true && au.Id == id
            //            select new ProfileResponse()
            //            {
            //                Id = au.Id,
            //                Name = au.Name,
            //                MiddleName = au.MiddleName,
            //                LastName = au.LastName,
            //                IsActive = au.IsActive,
            //                DateOfBirth = ci.DateOfBirth,
            //                CURP = ci.CURP,
            //                Sex = ci.Sex,
            //                Street = ad.Street,
            //                ExternalNumber = ad.ExternalNumber,
            //                InternalNumber = ad.InternalNumber,
            //                Description = sb.Description,
            //                PostalCode = sb.PostalCode,
            //                Suffering = mr.Suffering,
            //                Alergies = mr.Alergies,
            //                Medicines = mr.Medicines,
            //                BloodType = mr.BloodType,
            //                EmergencyContacts = emergencyContacts
            //            };
            #endregion

            var query = _context.appUsers
                .Join(_context.citizens, au => au.Id, ci => ci.AspNetUserId, (au, ci) => new { au, ci })
                .Join(_context.addresses, ci => ci.ci.CitizenId, ad => ad.CitizenId, (ci, ad) => new { ci, ad })
                .Join(_context.suburbs, ad => ad.ad.SuburbId, sb => sb.SuburbId, (ad, sb) => new { ad, sb })
                .Join(_context.medicalRecords, ci => ci.ad.ci.ci.CitizenId, mr => mr.CitizenId, (ci, mr) => new { ci, mr })
                .Where(w => w.ci.ad.ci.au.IsActive == true && w.ci.ad.ci.au.Id == id)
                .Select(s => new ProfileResponse
                {
                    Id = s.ci.ad.ci.au.Id,
                    Name = s.ci.ad.ci.au.Name,
                    MiddleName = s.ci.ad.ci.au.MiddleName,
                    LastName = s.ci.ad.ci.au.LastName,
                    IsActive = s.ci.ad.ci.au.IsActive,
                    DateOfBirth = s.ci.ad.ci.ci.DateOfBirth,
                    CURP = s.ci.ad.ci.ci.CURP,
                    Sex = s.ci.ad.ci.ci.Sex,
                    Street = s.ci.ad.ad.Street,
                    ExternalNumber = s.ci.ad.ad.ExternalNumber,
                    InternalNumber = s.ci.ad.ad.InternalNumber,
                    Description = s.ci.sb.Description,
                    PostalCode = s.ci.sb.PostalCode,
                    Alergies = s.mr.Alergies,
                    Suffering = s.mr.Suffering,
                    BloodType = s.mr.BloodType,
                    Medicines = s.mr.Medicines,
                    EmergencyContacts = _context.emergencyContacts
                        .Where(w => w.CitizenId == (
                            _context.appUsers.Join(
                            _context.citizens, ap => ap.Id, ci => ci.AspNetUserId, (ap, ci) => new { ap, ci })
                        .Where(w => w.ap.Id == id)
                        .Select(s => s.ci.CitizenId).FirstOrDefault()))
                        .ToList()
                });   
            return query;
        }
    }
}
