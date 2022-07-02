using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Data.Default
{
    public class Roles
    {
        public const string Admin = "Admin";
        public const string Owner = "Owner";
        public const string Renter = "Renter";
        public const string NormalUser = "User";
        public static SelectList GetRoles()
        {
            List<string> roles = new List<string>()
            {
                NormalUser,
                Admin,
                Owner,
                Renter
            };
            return new SelectList(roles);
        }
    }
}
