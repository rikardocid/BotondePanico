using GFDSystems.Vigitech.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFDSystems.Vigitech.DAO.Interfaces
{
    public interface IMedicalRecordDAO
    {

        Task<MedicalRecord> GetByIdAsync(int id);

        Task<MedicalRecord> CreateAsync(MedicalRecord medicalRecord);

        Task<MedicalRecord> UpdateAsync(MedicalRecord medicalRecord);

        Task DeleteAsync(MedicalRecord medicalRecord);

        Task<bool> ExistAsync(int id);
        Task<bool> ExistCitizenAsync(int id);

    }
}
