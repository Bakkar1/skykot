using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int HouseId { get; set; }
        public House House { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public string RoomType { get; set; }
        public int MaxPeople { get; set; }
        [Required]
        public decimal Period { get; set; }
        [Required]
        public DateTime AvailableFrom { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<RoomSpecification> RoomSpecifications { get; set; }
        public ICollection<RenterRoom> RenterRooms { get; set; }
        public ICollection<RoomImage> RoomImages { get; set; }
        public ICollection<RoomExpense> RoomExpenses { get; set; }
    }
}
