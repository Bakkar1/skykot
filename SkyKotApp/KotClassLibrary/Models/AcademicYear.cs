using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KotClassLibrary.Models
{
    public class AcademicYear
    {
        public int AcademicYearId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public ICollection<RenterRoom> RenterRooms { get; set; }
    }
}
