using System;

namespace GFDSystems.Vigitech.DAL.Responses
{
    public class DeviceAssignationResponse
    {
        //DeviceAssigned
        public int DeviceAssignedId { get; set; }
        public int CarLicence { get; set; }
        public int Description { get; set; }//num de patrulla
        public DateTime DateAsignation { get; set; }
        //statusdevice
        public int DescriptionStatusSevice { get; set; }
        //SecurityAgent
        public string AgentLicence { get; set; }
        public string Rank { get; set; }
        public string Corporation { get; set; }
        //aspnetuser
        public string FullName { get; set; }
    }
}