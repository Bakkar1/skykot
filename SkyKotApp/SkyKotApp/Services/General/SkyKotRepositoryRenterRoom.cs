using KotClassLibrary.Models;
using KotClassLibrary.ViewModels.RenterRoomVM;
using Microsoft.EntityFrameworkCore;
using SkyKotApp.Data.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Services.General
{
    public partial class SkyKotPartialRepository
    {
        public async Task<ICollection<RenterRoom>> GetRenterRooms()
        {
            if(GetCurrentUserRole() == Roles.Admin)
            {
                return await context
                    .RenterRooms
                    .Include(r => r.AcademicYear)
                    .Include(r => r.CustomUser)
                    .Include(r => r.Room)
                    .ToListAsync();
            }
            else if(GetCurrentUserRole() == Roles.Owner)
            {
                string id = GetCurrentUserId();
                return await context
                    .RenterRooms
                    .Include(r => r.AcademicYear)
                    .Include(r => r.CustomUser)
                    .Include(r => r.Room)
                    .Where(r => r.Room.House.UserHouses.Any(h => h.Id == GetCurrentUserId()))
                    .ToListAsync();
            }
            return null;
        }
        public async Task<RenterRoom> GetRenterRoom(int renterRoomId)
        {
            return await context.RenterRooms
                    .Include(r => r.AcademicYear)
                    .Include(r => r.CustomUser)
                    .Include(r => r.Room)
                    .Include(r => r.RenterContracts)
                    .FirstOrDefaultAsync(m => m.RenterRoomId == renterRoomId);
        }
        public async Task<RenterRoomCreateViewModel> GetRenterRoomCreateViewModel()
        {
            return new RenterRoomCreateViewModel()
            {
                AcademicYears = await context.AcademicYears.ToListAsync(),
                CustomUsers = await GetOwnCustomUsers(),
                Rooms = await GetRooms(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(12)
            };
        }
    }
}
