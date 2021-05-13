using System;
using System.Collections.Generic;
using System.Text;

namespace GFDSystems.Vigitech.DAO.Models
{
    public class UpdateInformationFromUserDAO
    {
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoleReversal { get; set; }
        public string OldRoleName { get; set; }
        public string NewRoleName { get; set; }
    }
}
