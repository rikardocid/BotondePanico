
namespace GFDSystems.Vigitech.DAL.Entities.MongoDB
{
    public class GpsTrackerNear
    {
        public string DeviceID { get; set; }
        public string CarLicence { get; set; }
        public string EconomicNumber { get; set; }
        public string GroupName { get; set; }
        public string Remark { get; set; }
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double Distance { get; set; }
    }
}
