using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace KotClassLibrary.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ZipCode> ZipCodes { get; set; }
    }
}
