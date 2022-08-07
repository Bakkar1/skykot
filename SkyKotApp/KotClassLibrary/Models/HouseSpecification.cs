using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Models
{
    public class HouseSpecification
    {
        public int HouseSpecificationId { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }
        public bool IsAvailAble { get; set; }
        public string WhereAvailAble { get; set; }
    }
}
