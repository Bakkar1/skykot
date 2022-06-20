using KotClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SkyKotApp.Data.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkyKotApp.Services.General
{
    public partial class SkyKotPartialRepository
    {
        public async Task<ICollection<House>> GetHouses()
        {
            string role = GetCurrentUserRole();
            if (role == Roles.Owner)
            {
                return await context.UserHouses
                  .Include(uh => uh.House.ZipCode)
                  .Where(uh => uh.Id == GetCurrentUserId())
                  .Select(uh => new House
                  {
                      HouseId = uh.HouseId,
                      Name = uh.House.Name,
                      Description = uh.House.Description,
                      HouseNumber = uh.House.HouseNumber,
                      StreetName = uh.House.StreetName,
                      ZipCode = uh.House.ZipCode
                  })
                   .ToListAsync();
            }
            else if (role == Roles.Admin)
            {
                 return await context.Houses
                    .Include(h => h.ZipCode)
                  .ToListAsync();
            }
            return null;
        }
       
        public async Task<House> AddHouse(House house)
        {
            await context.AddAsync(house);
            await context.SaveChangesAsync();
            string id = GetCurrentUserId();
            await context.UserHouses.AddAsync(new UserHouse
            {
                Id = GetCurrentUserId(),
                HouseId = house.HouseId
            });
            await context.SaveChangesAsync();
            return house;
        }
    }
}
