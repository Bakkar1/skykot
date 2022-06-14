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
    public class UserHouse
    {
        [Key]
        public int UserHouseId { get; set; }
        [ForeignKey("CustomUser")]
        public string IdentityUserId { get; set; }
        public CustomUser CustomUser { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }
    }
}
