using KotClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Services.Blazor
{
    public interface IBlazorRepository
    {
        Task<ICollection<Room>> GetRooms(string role, string userName);
        Task<ICollection<House>> GetHouses(string role, string userName);
        Task<ICollection<ZipCode>> GetZipCodes();
        Task<ICollection<CustomUser>> GetOwnCustomUsers(string userName);
        Task<bool> IsOwnRoom(string userName, int? roomId = 0);
        Task<bool> IsUserOwner(string userName, string userId);
    }
}
