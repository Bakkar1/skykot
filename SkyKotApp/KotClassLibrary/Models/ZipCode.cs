using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Models
{
    public class ZipCode
    {
        public int ZipCodeId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string City { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}
