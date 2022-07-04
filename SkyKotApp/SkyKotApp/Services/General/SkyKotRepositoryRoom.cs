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
                .Include(r => r.House)
                .Include(r => r.RoomImages)
                .Include(r => r.RoomSpecifications).ThenInclude(rs => rs.Specification)
                .Include(r => r.RoomExpenses).ThenInclude(re => re.Expence)
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
                HouseId = model.HouseId
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
            // update Room Specification
            if (model.RoomSpecificationsList != null)
            {
                foreach (var rSpecification in model.RoomSpecificationsList)
                {
                    if (rSpecification.RoomSpecificationId != 0)
                    {
                        RoomSpecification roomSpec = await context.RoomSpecifications.FindAsync(rSpecification.RoomSpecificationId);
                        roomSpec.IsAvailAble = string.IsNullOrWhiteSpace(rSpecification.WhereAvailAble) ? false : rSpecification.IsAvailAble;
                        roomSpec.WhereAvailAble = rSpecification.WhereAvailAble;

                        context.RoomSpecifications.Update(roomSpec);
                    }
                    else if (rSpecification.IsAvailAble)
                    {
                        await context.RoomSpecifications.AddAsync(new RoomSpecification()
                        {
                            RoomId = roomId,
                            SpecificationId = rSpecification.SpecificationId,
                            IsAvailAble = string.IsNullOrWhiteSpace(rSpecification.WhereAvailAble) ? false : rSpecification.IsAvailAble,
                            WhereAvailAble = rSpecification.WhereAvailAble
                        });
                    }
                }
            }
            // update Room Expenses
            if (model.RoomExpensesList != null)
            {
                foreach (var rExpense in model.RoomExpensesList)
                {
                    if (rExpense.Value != 0)
                    {
                        if (rExpense.RoomExpenseId != 0)
                        {
                            RoomExpense roomExpense = await context.RoomExpenses.FindAsync(rExpense.RoomExpenseId);
                            roomExpense.Value = rExpense.Value;
                            context.RoomExpenses.Update(roomExpense);
                        }
                        else
                        {
                            await context.RoomExpenses.AddAsync(new RoomExpense()
                            {
                                ExpenceId = rExpense.ExpenceId,
                                RoomId = roomId,
                                Value = rExpense.Value
                            });
                        }
                    }
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
                IsAvailable = model.IsAvailable
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
                //add spec
                if (model.RoomSpecificationsList != null)
                {
                    foreach (var rSpecification in model.RoomSpecificationsList)
                    {
                        if (rSpecification.IsAvailAble)
                        {
                            await context.RoomSpecifications.AddAsync(new RoomSpecification()
                            {
                                RoomId = roomId,
                                SpecificationId = rSpecification.SpecificationId,
                                IsAvailAble = string.IsNullOrWhiteSpace(rSpecification.WhereAvailAble) ? false : rSpecification.IsAvailAble,
                                WhereAvailAble = rSpecification.WhereAvailAble
                            });
                        }
                    }
                }

                //add expence
                if (model.RoomExpensesList != null)
                {
                    foreach (var rExpense in model.RoomExpensesList)
                    {
                        if (rExpense.Value != 0)
                        {
                            await context.RoomExpenses.AddAsync(new RoomExpense()
                            {
                                ExpenceId = rExpense.ExpenceId,
                                RoomId = roomId,
                                Value = rExpense.Value
                            });
                        }
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
        public async Task<List<RoomSpecification>> GetRoomSpecificationsToCreate()
        {
            List<RoomSpecification> roomSpecifications = new List<RoomSpecification>();
            foreach(Specification specification in await GetSpecifications())
            {
                roomSpecifications.Add(new RoomSpecification()
                {
                    SpecificationId = specification.SpecificationId,
                    IsAvailAble = false,
                    Specification = new Specification()
                    {
                        Description = specification.Description
                    }
                }) ;
            }
            return roomSpecifications;
        }
        public async Task<List<RoomSpecification>> GetRoomSpecificationsToEdit(ICollection<RoomSpecification> roomSpecifications)
        {
            foreach (Specification specification in await GetSpecifications())
            {
                RoomSpecification roomSpecification = roomSpecifications.Where(rm => rm.Specification.SpecificationId == specification.SpecificationId).FirstOrDefault();
                if(roomSpecification == null)
                {
                    roomSpecifications.Add(new RoomSpecification()
                    {
                        SpecificationId = specification.SpecificationId,
                        IsAvailAble = false,
                        Specification = new Specification()
                        {
                            Description = specification.Description
                        }
                    });
                }
            }
            return roomSpecifications.ToList();
        }

        public async Task<List<RoomExpense>> GetRoomExpenseToCreate()
        {
            List<RoomExpense> roomExpenses = new List<RoomExpense>();
            foreach(Expence expence in await GetExpences())
            {
                roomExpenses.Add(new RoomExpense()
                {
                    ExpenceId = expence.ExpenceId,
                    Expence = new Expence()
                    {
                        Description = expence.Description
                    }
                });
            }
            return roomExpenses;
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
