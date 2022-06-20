using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KotClassLibrary.Models
{
    public class RoomSpecification
    {
        public int RoomSpecificationId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int SpecificationId { get; set; }
        public Specification Specification { get; set; }
        public bool IsAvailAble { get; set; }
        public string WhereAvailAble { get; set; }
    }
}
