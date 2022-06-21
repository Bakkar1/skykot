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
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int RoomNumber { get; set; }
        [Required]
        public string RoomType { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int MaxPeople { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Period { get; set; }
        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        //public int Price { get; set; }
        [Required]
        public DateTime AvailableFrom { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        public string Description { get; set; }

        public ICollection<RoomSpecification> RoomSpecifications { get; set; }
        public ICollection<RenterRoom> RenterRooms { get; set; }
        public ICollection<RoomImage> RoomImages { get; set; }
        public ICollection<RoomExpense> RoomExpenses { get; set; }
    }
}
