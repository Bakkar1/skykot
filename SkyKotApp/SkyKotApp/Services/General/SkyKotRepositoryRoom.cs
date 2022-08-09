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
        public async Task<ICollection<Room>> GetRooms()
        {
            string role = GetCurrentUserRole();
            switch (role)
            {
                case Roles.Admin:
                    return await context.Rooms
                    .Include(r => r.House)
                    .Include(r => r.RoomImages)
                    .ToListAsync();
                case Roles.Owner:
                    return await context.Rooms
                    .Include(r => r.House)
                    .Include(r => r.RoomImages)
                    .Where(r => r.House.UserHouses.Any(us => us.Id == GetCurrentUserId()))
                    .ToListAsync();
                default:
                    return null;

            }
        }
        public async Task<ICollection<Room>> GetRoomsForHome(int count)
        {
            return await context.Rooms
                .Include(r => r.House.ZipCode)
                .Include(r => r.RoomImages)
                .Where(r => r.IsAvailable)
                .OrderByDescending(r => r.RoomId)
                .Take(count)
                .ToListAsync();
        }
        public async Task<Room> GetRoom(int roomId)
        {
            return await context.Rooms
                .Include(r => r.House).ThenInclude(r => r.HouseExpenses).ThenInclude(e => e.Expence)
                .Include(r => r.House).ThenInclude(r => r.HouseSpecifications).ThenInclude(s => s.Specification)
                .Include(r => r.RoomImages)
                .FirstOrDefaultAsync(m => m.RoomId == roomId);
        }
        public async Task<Room> UpdateRoom(RoomEditViewModel model)
        {
            //update room
            Room room = new Room()
            {
                RoomId = model.RoomId,
                Price = model.Price,
                Description = model.Description,
                RoomNumber = model.RoomNumber,
                RoomType = model.RoomType,
                MaxPeople = model.MaxPeople,
                Period = model.Period,
                AvailableFrom = model.AvailableFrom,
                IsAvailable = model.IsAvailable,
                HouseId = model.HouseId,
                Surface = model.Surface
            };
            context.Update(room);
            
            int roomId = room.RoomId;

            // add images
            if (model.ImagesPaths != null)
            {
                foreach (var path in model.ImagesPaths)
                {
                    await context.RoomImages.AddAsync(new RoomImage()
                    {
                        RoomId = roomId,
                        Path = path
                    });
                }
            }
            await context.SaveChangesAsync();
            return room;
        }
        public async Task<Room> AddRoom(RoomCreateViewModel model)
        {
            Room room = new Room()
            {
                HouseId = model.HouseId,
                Price = model.Price,
                Description = model.Description,
                RoomNumber = model.RoomNumber,
                RoomType = model.RoomType,
                MaxPeople = model.MaxPeople,
                Period = model.Period,
                AvailableFrom = model.AvailableFrom,
                IsAvailable = model.IsAvailable,
                Surface = model.Surface
            };
            await context.Rooms.AddAsync(room);
            await context.SaveChangesAsync();
            int roomId = room.RoomId;
            if (roomId != 0)
            {
                // add images
                if (model.ImagesPaths != null)
                {
                    foreach(var path in model.ImagesPaths)
                    {
                        await context.RoomImages.AddAsync(new RoomImage()
                        {
                            RoomId = roomId,
                            Path = path
                        }) ;
                    }
                }

                await context.SaveChangesAsync();
            }
            return room;
        }
        public async Task<ICollection<Specification>> GetSpecifications()
        {
            return await context.Specifications.ToListAsync();
        }
        public async Task<ICollection<Expence>> GetExpences()
        {
            return await context.Expences.ToListAsync();
        }
        public async Task<SelectList> GetSpecificationsSelect()
        {
            return new SelectList(await context.Specifications.ToListAsync(), nameof(Specification.SpecificationId), nameof(Specification.Description));
        }
        public async Task<SelectList> GetExpensesSelect()
        {
            return new SelectList(await context.Expences.ToListAsync(), nameof(Expence.ExpenceId), nameof(Expence.Description));
        }
    }
}
