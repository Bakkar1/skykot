using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Services.General
{
    public partial class SkyKotPartialRepository
    {
        public async Task<bool> IsOwnRoom(int? roomId = 0)
        {
            return await context.Rooms
                        .AnyAsync(r => r.House.UserHouses.Where(us => us.Id == GetCurrentUserId()).Any() && r.RoomId == roomId);
        }

        public async Task<bool> IsOwnHouseAsync(int? houseId = 0)
        {
            return await context.UserHouses.AnyAsync(us => us.Id == GetCurrentUserId() && us.HouseId == houseId);
        }
    }
}
