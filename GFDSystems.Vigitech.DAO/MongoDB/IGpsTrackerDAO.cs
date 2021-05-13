using GFDSystems.Vigitech.DAL.Entities.MongoDB;
using GFDSystems.Vigitech.DAL.Entities.Responses;
using System.Collections.Generic;

namespace GFDSystems.Vigitech.DAO.MongoDB
{
    public interface IGpsTrackerDAO
    {
        IList<LocationResponse> GetAll();
        IList<GpsTrackerNear> GetNear(double lat, double lon);
        IList<GpsTracker> GetByIdAsync(string id);
        //Task<GpsTracker> CreateAsync(GpsTracker gpsTracker);
        //Task<GpsTracker> UpdateAsync(GpsTracker gpsTracker);
        //Task DeleteAsync(GpsTracker gpsTracker);
        //Task<bool> SaveAllAsync();
    }
}
