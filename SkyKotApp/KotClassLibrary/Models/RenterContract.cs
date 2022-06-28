using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Models
{
    public class RenterContract
    {
        public int RenterContractId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public bool IsPayed { get; set; }
        public int RenterRoomId { get; set; }
        public RenterRoom RenterRoom { get; set; }
    }
}
