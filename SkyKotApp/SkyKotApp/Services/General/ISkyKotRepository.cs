using KotClassLibrary.Models;
using KotClassLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        Task<SelectList> GetHousesSelectList();
        Task<House> AddHouse(House house);
        #endregion
        #region Room
        Task<ICollection<Room>> GetRooms();
        Task<Room> GetRoom(int roomId);
        Task<Room> AddRoom(RoomCreateViewModel model);
        Task<Room> UpdateRoom(RoomEditViewModel model);
        Task<List<RoomSpecification>> GetRoomSpecificationsToCreate();
        Task<List<RoomExpense>> GetRoomExpenseToCreate();

        Task<List<RoomSpecification>> GetRoomSpecificationsToEdit(ICollection<RoomSpecification> roomSpecifications);

        Task<ICollection<Specification>> GetSpecifications();
        Task<ICollection<Expence>> GetExpences();
        Task<SelectList> GetSpecificationsSelect();
        Task<SelectList> GetExpensesSelect();
        #endregion
    }
}
