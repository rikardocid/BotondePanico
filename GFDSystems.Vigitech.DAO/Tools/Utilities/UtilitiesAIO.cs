using GFDSystems.Vigitech.DAO.Tools.ErrorCodes;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GFDSystems.Vigitech.DAO.Tools.Utilities
{
    public static class UtilitiesAIO
    {
        public static string GetCallerMemberName([CallerMemberName] string name = "")
        {
            return name;
        }

        public static List<string> AddErrors(IdentityResult result)
        {
            List<string> builder = new List<string>();
            foreach (var error in result.Errors)
            {
                string data = IdentityErrorCodes.All.FirstOrDefault(x => x.Key == error.Code).Value;
                if (!string.IsNullOrEmpty(data))
                {
                    builder.Add(data);
                }
            }

            return builder;
        }
    }
}
