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
        public async Task<ICollection<RenterContract>> GetRenterContracts(int roomId)
        {
            return await context.RenterContracts
                .Include(rc => rc.RenterRoom)
                .Where(rc => rc.RenterRoom.RoomId == roomId)
                .OrderBy(rc => rc.StartDate)
                .ToListAsync();
        }
        public async Task<RenterContract> GetRenterContract(int renterContractId)
        {
            if (GetCurrentUserRole() == Roles.Renter)
            {
                return await context.RenterContracts
                .Include(rc => rc.RenterRoom)
                .Where(rc => rc.RenterRoom.Id == GetCurrentUserId())
                .FirstOrDefaultAsync(rc => rc.RenterContractId == renterContractId);
            }
            else if (GetCurrentUserRole() == Roles.Owner)
            {

                RenterContract renterContract = await context.RenterContracts
                    .Include(rc => rc.RenterRoom)
                    .FirstOrDefaultAsync(rc => rc.RenterContractId == renterContractId);
                if (renterContract != null)
                {
                    if (await IsOwnRenterRoom(renterContract.RenterRoomId))
                    {
                        return renterContract;
                    }
                }
            }
            else
            {
                return await context.RenterContracts
                .Include(rc => rc.RenterRoom)
                .FirstOrDefaultAsync(rc => rc.RenterContractId == renterContractId);
            }
            return null;
        }
        public async Task<RenterContract> UpdateRenterContract(RenterContract renterContract)
        {
            context.RenterContracts.Update(renterContract);
            await context.SaveChangesAsync();
            return renterContract;
        }
    }
}
