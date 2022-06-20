using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Models
{
    public class House
    {
        public int HouseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ZipCodeId { get; set; }
        public ZipCode ZipCode { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        public string Description { get; set; }
        public ICollection<UserHouse> UserHouses { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
