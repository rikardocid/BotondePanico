using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GFDSystems.Vigitech.DAL.Models
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }

        //Add field to role

    }
}
