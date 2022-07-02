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
        public async Task<RenterContract> GetRenterContract(int renterContractId)
        {
            if (GetCurrentUserRole() == Roles.Renter)
            {
                return await context.RenterContracts
                .Include(rc => rc.RenterRoom)
                .Where(rc => rc.RenterRoom.Id == GetCurrentUserId())
                .FirstOrDefaultAsync(rc => rc.RenterContractId == renterContractId);
            }
            else if (GetCurrentUserRole() == Roles.Admin)
            {
                return await context.RenterContracts
                .Include(rc => rc.RenterRoom)
                .FirstOrDefaultAsync(rc => rc.RenterContractId == renterContractId);
            }
            return null; ;
        }
        public async Task<RenterContract> UpdateRenterContract(RenterContract renterContract)
        {
            context.RenterContracts.Update(renterContract);
            await context.SaveChangesAsync();
            return renterContract;
        }
    }
}
