using KotClassLibrary.Models;
using KotClassLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<ICollection<RenterRoom>> GetRenters()
        {
            if(GetCurrentUserRole() == Roles.Renter)
            {
                return await context.RenterRooms
                .Include(r => r.Room)
                .Include(r => r.RenterContracts)
                .Include(r => r.Room.House)
                .Include(r => r.Room.RoomImages)
                .Include(r => r.Room.RoomExpenses)
                .Where(r => r.Id == GetCurrentUserId())
                .ToListAsync();
            }
            else if( GetCurrentUserRole() == Roles.Admin)
            {
                return await context.RenterRooms
                .Include(r => r.Room)
                .Include(r => r.RenterContracts)
                .Include(r => r.Room.House)
                .Include(r => r.Room.RoomImages)
                .Include(r => r.Room.RoomExpenses)
                .ToListAsync();
            }
            return null;
        }
        public async Task<RenterRoom> GetRenter(int renterRoomId)
        {
            if (GetCurrentUserRole() == Roles.Renter)
            {
                return await context.RenterRooms
                    .Include(r => r.RenterContracts)
                    .Include(r => r.Room)
                    .Include(r => r.RenterContracts)
                    .Include(r => r.Room.House)
                    .Include(r => r.Room.RoomImages)
                    .Include(r => r.Room.RoomExpenses)
                    .Where(r => r.Id == GetCurrentUserId())
                    .FirstOrDefaultAsync(r => r.RenterRoomId == renterRoomId);
            }
            else if (GetCurrentUserRole() == Roles.Admin)
            {
                return await context.RenterRooms
                    .Include(r => r.RenterContracts)
                    .Include(r => r.Room)
                    .Include(r => r.RenterContracts)
                    .Include(r => r.Room.House)
                    .Include(r => r.Room.RoomImages)
                    .Include(r => r.Room.RoomExpenses)
                    .FirstOrDefaultAsync(r => r.RenterRoomId == renterRoomId);
            }
            return null;
        }
    }
}
