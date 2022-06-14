using KotClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Services.General
{
    public interface ISkyKotRepository
    {
        #region User
        Task<ICollection<CustomUser>> GetCustomUsers();
        string GetCurrentUserId();
        string GetCurrentUserRole();
        Task<CustomUser> GetUser(string id);
        Task<CustomUser> GetUserByName(string userName);
        #endregion
        #region IsOwn
        Task<bool> IsOwnHouseAsync(int? houseId);
        Task<bool> IsOwnRoom(int? roomId);
        #endregion
        #region House
        Task<ICollection<House>> GetHouses();
        Task<House> AddHouse(House house);
        #endregion
    }
}
