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
    public class RenterRoom
    {
        [Key]
        public int RenterRoomId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        [ForeignKey("CustomUser")]
        public string Id { get; set; }
        public CustomUser CustomUser { get; set; }
        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public double AmountToPay { get; set; }
        public byte[] Contract { get; set; }
        public ICollection<RenterContract> RenterContracts { get; set; }

    }
}
