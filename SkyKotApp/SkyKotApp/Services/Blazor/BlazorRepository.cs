using KotClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SkyKotApp.Data;
using SkyKotApp.Data.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkyKotApp.Services.Blazor
{
    public class BlazorRepository : IBlazorRepository
    {
        private readonly AppDbContext context;

        public BlazorRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<Room>> GetRooms(string role, string userName)
        {
            string id = await GetUserIdByName(userName);

            if (role == Roles.Owner)
            {
                return await context.Rooms
                    .Include(r => r.House.ZipCode)
                    .Include(r => r.RoomImages)
                    .Include(r => r.House.HouseExpenses)
                    .Where(r => r.House.UserHouses.Any(us => us.Id == id))
                    .ToListAsync();
            }
            else if (role == Roles.Admin)
            {
                return await context.Rooms
                    .Include(r => r.House.ZipCode)
                    .Include(r => r.RoomImages)
                    .Include(r => r.House.HouseExpenses)
                    .ToListAsync();
            }
            else
            {
                return await context.Rooms
                .Include(r => r.House.ZipCode)
                .Include(r => r.RoomImages)
                .Include(r => r.House.HouseExpenses)
                .Where(r => r.IsAvailable)
                .ToListAsync();
            }
        }
        public async Task<ICollection<House>> GetHouses(string role, string userName)
        {
            string id = await GetUserIdByName(userName);

            if (role == Roles.Owner)
            {
                return await context.UserHouses
                  .Include(uh => uh.House.ZipCode)
                  .Where(uh => uh.Id == id)
                  .Select(uh => new House
                  {
                      HouseId = uh.HouseId,
                      Name = uh.House.Name,
                      Description = uh.House.Description,
                      HouseNumber = uh.House.HouseNumber,
                      StreetName = uh.House.StreetName,
                      ZipCode = uh.House.ZipCode,
                      ContractRules = uh.House.ContractRules
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
        public async Task<ICollection<ZipCode>> GetZipCodes()
        {
            return await context
                .ZipCodes
                .Include(z => z.Country)
                .Select(z => new ZipCode() { ZipCodeId = z.ZipCodeId, City = z.City, Country = new Country() { Name = z.Country.Name } })
                .ToListAsync();
        }

        private async Task<string> GetUserIdByName(string userName)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return user == null ? "" : user.Id.ToString();
        }

        // create contract
        public async Task<ICollection<CustomUser>> GetOwnCustomUsers(string userName)
        {
            string id = await GetUserIdByName(userName);
            return await context
                .Users
                .Where(u => u.OwnerId == id)
                .ToListAsync();
        }

        public async Task<bool> IsOwnRoom(string userName, int? roomId = 0)
        {
            string id = await GetUserIdByName(userName);
            return await context.Rooms
                        .AnyAsync(r => r.House.UserHouses.Where(us => us.Id == id).Any() && r.RoomId == roomId);
        }
        public async Task<bool> IsUserOwner(string userName,string userId)
        {
            string id = await GetUserIdByName(userName);
            return await context.Users
                .AnyAsync(u =>
                u.Id == userId &&
                u.OwnerId == id);
        }

    }
}
