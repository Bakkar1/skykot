using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KotClassLibrary.Models
{
    public class Specification
    {
        public int SpecificationId { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection<RoomSpecification> RoomSpecifications { get; set; }
        public ICollection<HouseSpecification> HouseSpecifications { get; set; }
    }
}
