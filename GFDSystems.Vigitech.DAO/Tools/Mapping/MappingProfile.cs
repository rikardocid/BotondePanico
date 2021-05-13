using AutoMapper;
using GFDSystems.Vigitech.DAO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Tools.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressInfo, GFDSystems.Vigitech.DAL.Entities.Address>().ReverseMap();
            CreateMap<MedicalInfo, GFDSystems.Vigitech.DAL.Entities.MedicalRecord>().ReverseMap();
            CreateMap<ContactInfo, GFDSystems.Vigitech.DAL.Entities.EmergencyContact>().ReverseMap();
            CreateMap<NotificationBaseDAO, GFDSystems.Vigitech.DAL.Entities.NotificationBase>().ReverseMap();
        }
    }
}
