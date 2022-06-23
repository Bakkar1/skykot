﻿using KotClassLibrary.Models;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlazorRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<ICollection<Room>> GetRooms()
        {
            string role = GetCurrentUserRole();
            if (role == Roles.Owner)
            {
                return await context.Rooms
                    .Include(r => r.House)
                    .Include(r => r.RoomImages)
                    .Where(r => r.House.UserHouses.Any(us => us.Id == GetCurrentUserId()))
                    .ToListAsync();
            }
            else if (role == Roles.Admin)
            {
                return await context.Rooms
                    .Include(r => r.House)
                    .Include(r => r.RoomImages)
                    .ToListAsync();
            }
            else
            {
                return await context.Rooms
                .Include(r => r.House.ZipCode)
                .Include(r => r.RoomImages)
                .Where(r => r.IsAvailable)
                .ToListAsync();
            }
        }
        public string GetCurrentUserRole()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
