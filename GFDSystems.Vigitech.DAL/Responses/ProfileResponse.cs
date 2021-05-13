using System;
using System.Collections.Generic;

namespace GFDSystems.Vigitech.DAL.Entities.Responses
{
    public class ProfileResponse
    {
        //appuser
        public int Id { get; set; }//aspnetuser
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }///politicas de privacidad de datos 

        //Citizen
        public DateTime DateOfBirth { get; set; }
        public string CURP { get; set; }
        public string Sex { get; set; }

        //address
        public string Street { get; set; }
        public string ExternalNumber { get; set; }
        public string InternalNumber { get; set; }

        //suburb 
        public string Description { get; set; }
        public int PostalCode { get; set; }

        //medicalRecord
        public string Suffering { get; set; }
        public string Alergies { get; set; }
        public string Medicines { get; set; }
        public string BloodType { get; set; }
        //EmergenciContact
        public List<EmergencyContact> EmergencyContacts { get; set; }

    }
}
