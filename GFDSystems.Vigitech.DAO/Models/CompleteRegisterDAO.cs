using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Models
{
    public class CompleteRegisterDAO
    {
        public CompleteRegisterDAO()
        {
            ContactInfos = new List<ContactInfo>();
        }

        [Required]
        public string DeviceId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public AddressInfo AddressInfo { get; set; }
        public MedicalInfo MedicalInfo { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }
    }

    public class PersonalInfo
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Curp { get; set; }
        [Required]
        public string Sex { get; set; }
    }

    public class AddressInfo
    {
        [Required]
        public int PostalCode { get; set; }
        [Required]
        public int suburbId { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string ExternalNumber { get; set; }
        public string InternalNumber { get; set; }
    }

    public class MedicalInfo
    {
        public string Suffering { get; set; }
        public string Alergies { get; set; }
        public string Medicines { get; set; }
        public string BloodType { get; set; }

    }

    public class ContactInfo
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
    }
}
