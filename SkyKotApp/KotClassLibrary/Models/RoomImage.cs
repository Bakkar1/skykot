using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KotClassLibrary.Models
{
    public class RoomImage
    {
        public int RoomImageId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
