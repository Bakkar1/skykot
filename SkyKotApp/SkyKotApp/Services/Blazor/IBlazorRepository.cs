using KotClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Services.Blazor
{
    public interface IBlazorRepository
    {
        Task<ICollection<Room>> GetRooms();
        Task<ICollection<House>> GetHouses();
    }
}
