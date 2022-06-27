using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Models
{
    public class CustomUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        [ForeignKey("IdentityUser")]
        public string OwnerId { get; set; }

        public ICollection<UserHouse> UserHouses { get; set; }
        public ICollection<RenterRoom> RenterRooms { get; set; }
    }
}
