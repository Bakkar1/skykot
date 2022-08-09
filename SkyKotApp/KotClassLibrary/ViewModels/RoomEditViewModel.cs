using KotClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.ViewModels
{
    public class RoomEditViewModel : Room
    {
        public RoomEditViewModel()
        {

        }
        public RoomEditViewModel(Room room)
        {
            RoomId = room.RoomId;
            Price = room.Price;
            Description = room.Description;
            RoomNumber = room.RoomNumber;
            RoomType = room.RoomType;
            MaxPeople = room.MaxPeople;
            Period = room.Period;
            AvailableFrom = room.AvailableFrom;
            IsAvailable = room.IsAvailable;
            HouseId = room.HouseId;
            House = room.House;
            RoomImages = room.RoomImages;
            Surface = room.Surface;
        }
        public List<IFormFile> Photos { get; set; }
        public List<string> ImagesPaths { get; set; }
        public SelectList HousesSelectList { get; set; }
    }
}
