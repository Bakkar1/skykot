using KotClassLibrary.Models;
using KotClassLibrary.ViewModels;
using KotClassLibrary.ViewModels.RenterRoomVM;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        Task<ICollection<CustomUser>> GetOwnCustomUsers();
        string GetCurrentUserId();
        string GetCurrentUserRole();
        Task AddToRenterRole(CustomUser identityUser);
        Task<CustomUser> GetUser(string id);
        Task<CustomUser> UpdateUser(CustomUser user);
        Task<CustomUser> DeleteUser(string id);
        Task<CustomUser> GetUserByName(string userName);
        Task<bool> IsAlredyEmailExist(string email, string userId);
        #endregion

        #region Role
        public SelectList GetRoles();
        string GetRoleName(string UserId);
        Task AddToRole(CustomUser identityUser, string role);
        void DeleteOldRole(string UserId);
        Task UpdateRole(CustomUser identityUser, string roleId);
        #endregion

        #region IsOwn
        Task<bool> IsOwnHouseAsync(int? houseId);
        Task<bool> IsOwnRenterRoom(int renterRoomId);
        Task<bool> IsOwnRoom(int? roomId);
        Task<bool> IsUserOwner(string userId);
        #endregion

        #region House
        Task<ICollection<House>> GetHouses();
        Task<SelectList> GetHousesSelectList();
        Task<House> AddHouse(House house);
        #endregion

        #region Room
        Task<ICollection<Room>> GetRooms();
        Task<ICollection<Room>> GetRoomsForHome(int count = 5);
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

        #region RenterRoom
        Task<ICollection<RenterRoom>> GetRenterRooms();
        Task<RenterRoom> GetRenterRoom(int renterRoomId);
        Task<RenterRoom> CreateRenterRoom(RenterRoomCreateViewModel model);
        Task<RenterRoom> UpdateRenterRoom(RenterRoomEditViewModel model);
        Task<RenterRoomCreateViewModel> GetRenterRoomCreateViewModel();
        Task<RenterRoomCreateViewModel> GetRenterRoomCreateViewModel(RenterRoomCreateViewModel room);
        Task<RenterRoomEditViewModel> GetRenterRoomEditViewModel(RenterRoom renterRoom);
        Task<RenterRoomEditViewModel> GetRenterRoomEditViewModel(RenterRoomEditViewModel renterRoomEditViewModel);
        Task<bool> Checkoverlapping(RenterRoomCreateViewModel model);
        Task<Dictionary<string, string>> CheckoverlappingModalError(RenterRoomCreateViewModel model);
        Task<Dictionary<string, string>> CheckoverlappingModalError(RenterRoomEditViewModel model);
        #endregion

        #region Renter
        Task<ICollection<RenterRoom>> GetRenters();
        Task<RenterRoom> GetRenter(int renterRoomId);
        #endregion
        #region RenterContract
        Task<RenterContract> GetRenterContract(int renterContractId);
        Task<RenterContract> UpdateRenterContract(RenterContract renterContract);
        #endregion
    }
}
