using KotClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using SkyKotApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ICollection<Room>> GetRooms()
        {
            return await context.Rooms
                .Include(r => r.House.ZipCode)
                .Include(r => r.RoomImages)
                .Where(r => r.IsAvailable)
                .ToListAsync();
        }
        public async Task<ICollection<House>> GetHouses()
        {
            return await context
                .Houses
                .Select(x => new House() { HouseId = x.HouseId, Name = x.Name})
                .ToListAsync();
        }
        public async Task<ICollection<ZipCode>> GetZipCodes()
        {
            return await context
                .ZipCodes
                .Include(z => z.Country)
                .Select(z => new ZipCode() { ZipCodeId = z.ZipCodeId, City = z.City, Country = new Country() { Name = z.Country.Name } })
                .ToListAsync();
        }
    }
}
