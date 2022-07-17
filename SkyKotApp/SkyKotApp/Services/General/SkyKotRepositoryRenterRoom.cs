using KotClassLibrary.Models;
using KotClassLibrary.ViewModels.RenterRoomVM;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public async Task<ICollection<RenterRoom>> GetRenterRooms()
        {
            if(GetCurrentUserRole() == Roles.Admin)
            {
                return await context
                    .RenterRooms
                    .Include(r => r.AcademicYear)
                    .Include(r => r.CustomUser)
                    .Include(r => r.Room)
                    .ToListAsync();
            }
            else if(GetCurrentUserRole() == Roles.Owner)
            {
                string id = GetCurrentUserId();
                return await context
                    .RenterRooms
                    .Include(r => r.AcademicYear)
                    .Include(r => r.CustomUser)
                    .Include(r => r.Room)
                    .Where(r => r.Room.House.UserHouses.Any(h => h.Id == GetCurrentUserId()))
                    .ToListAsync();
            }
            return null;
        }
        public async Task<RenterRoom> GetRenterRoom(int renterRoomId)
        {
            return await context.RenterRooms
                    .Include(r => r.AcademicYear)
                    .Include(r => r.CustomUser)
                    .Include(r => r.Room)
                    .Include(r => r.RenterContracts)
                    .FirstOrDefaultAsync(m => m.RenterRoomId == renterRoomId);
        }
        public async Task<RenterRoom> CreateRenterRoom(RenterRoomCreateViewModel model)
        {
            Room room = await GetRoom(model.RoomId);
            var toPay = room.RoomExpenses.Sum(re => re.Value) + room.Price;
            model.AmountToPay = toPay;

            RenterRoom renterRoom = model;
            context.Add(renterRoom);
            await context.SaveChangesAsync();

            int renterRoomId = model.RenterRoomId;

            DateTime startDate = model.StartDate;
            DateTime endDate = model.EndDate;

            while (startDate <= endDate)
            {
                context.RenterContracts.Add(new RenterContract()
                {
                    StartDate = startDate,
                    RenterRoomId = renterRoomId,
                    IsPayed = false
                });
                startDate = startDate.AddMonths(1);
            }
            await context.SaveChangesAsync();

            return renterRoom;
        }
        public async Task<RenterRoomCreateViewModel> GetRenterRoomCreateViewModel()
        {
            var model = new RenterRoomCreateViewModel()
            {
                AcademicYears = await context.AcademicYears.ToListAsync(),
                Rooms = await GetRooms(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(12)
            };
            if(GetCurrentUserRole() == Roles.Admin)
            {
                model.CustomUsers = await GetCustomUsers();
            }
            else
            {
                model.CustomUsers = await GetOwnCustomUsers();
            }
            return model;
        }
        public async Task<RenterRoomCreateViewModel> GetRenterRoomCreateViewModel(RenterRoomCreateViewModel renterRoomCreateViewModel)
        {
            renterRoomCreateViewModel.AcademicYears = await context.AcademicYears.ToListAsync();
            renterRoomCreateViewModel.Rooms = await GetRooms();
            if (GetCurrentUserRole() == Roles.Admin)
            {
                renterRoomCreateViewModel.CustomUsers = await GetCustomUsers();
            }
            else
            {
                renterRoomCreateViewModel.CustomUsers = await GetOwnCustomUsers();
            }
            return renterRoomCreateViewModel;
        }
        public async Task<RenterRoomEditViewModel> GetRenterRoomEditViewModel(RenterRoom renterRoom)
        {
            RenterRoomEditViewModel model = new RenterRoomEditViewModel()
            {
                RenterRoomId = renterRoom.RenterRoomId,
                RoomId = renterRoom.RoomId,
                Id = renterRoom.Id,
                StartDate = renterRoom.StartDate,
                AmountToPay = renterRoom.AmountToPay,
                EndDate = renterRoom.EndDate,
                AcademicYearId = renterRoom.AcademicYearId,
                AcademicYears = await context.AcademicYears.ToListAsync(),
                Rooms = await GetRooms(),
            };
            if (GetCurrentUserRole() == Roles.Admin)
            {
                model.CustomUsers = await GetCustomUsers();
            }
            else
            {
                model.CustomUsers = await GetOwnCustomUsers();
            }

            return model;
        }
        public async Task<RenterRoomEditViewModel> GetRenterRoomEditViewModel(RenterRoomEditViewModel renterRoomEditViewModel)
        {
            renterRoomEditViewModel.AcademicYears = await context.AcademicYears.ToListAsync();
            renterRoomEditViewModel.Rooms = await GetRooms();

            if (GetCurrentUserRole() == Roles.Admin)
            {
                renterRoomEditViewModel.CustomUsers = await GetCustomUsers();
            }
            else
            {
                renterRoomEditViewModel.CustomUsers = await GetOwnCustomUsers();
            }

            return renterRoomEditViewModel;
        }

        public async Task<RenterRoom> UpdateRenterRoom(RenterRoomEditViewModel model)
        {
            RenterRoom renterRoom = await context.RenterRooms.FindAsync(model.RenterRoomId);
            if(renterRoom != null)
            {
                renterRoom.RoomId = model.RoomId;
                renterRoom.Id = model.Id;
                renterRoom.AcademicYearId = model.AcademicYearId;
                renterRoom.StartDate = model.StartDate;
                renterRoom.EndDate = model.EndDate;
                context.Update(renterRoom);
                await context.SaveChangesAsync();
            }
            return renterRoom;
        }

        public async Task<bool> Checkoverlapping(RenterRoomCreateViewModel model)
        {
            if(await context.RenterRooms.AnyAsync(rr => rr.RoomId == model.RoomId && rr.AcademicYearId == model.AcademicYearId))
            {
                return false;
            }

            List<RenterRoom> renterRooms =  await context.RenterRooms
                .Where(rr => rr.RoomId == model.RoomId)
                .OrderBy(rr => rr.StartDate)
                .ToListAsync();

            
            int i = 0;
            if (model.StartDate >= model.EndDate)
            {
                return false;
            }
            else if(renterRooms != null)
            {
                foreach(var r in renterRooms)
                {
                    DateTime nextStartDate = i < renterRooms.Count ? renterRooms[i].StartDate : new DateTime();

                    if (model.StartDate == r.StartDate || model.EndDate == r.EndDate)
                    {
                        return false;
                    }
                    else if (model.StartDate > r.StartDate && model.EndDate < r.EndDate)
                    {
                        return false;
                    }
                    else if (model.StartDate < r.StartDate)
                    {
                        if(model.EndDate >= r.StartDate)
                        {
                            return false;
                        }
                    }
                    else if(model.StartDate > r.EndDate)
                    {
                        if(nextStartDate != DateTime.MinValue)
                        {
                            if (model.EndDate > nextStartDate && nextStartDate > model.StartDate)
                            {
                                return false;
                            }
                        }
                    }
                    else if (model.StartDate > r.StartDate)
                    {
                        if (model.EndDate <= r.EndDate || model.StartDate <= model.EndDate)
                        {
                            return false;
                        }
                    }
                    i++;
                }
            }
            return true;
        }  

        public async Task<Dictionary<string, string>> CheckoverlappingModalError(RenterRoomCreateViewModel model)
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            if (await context.RenterRooms.AnyAsync(rr => rr.RoomId == model.RoomId && rr.AcademicYearId == model.AcademicYearId))
            {
                lst.Add("AcademicYearId", "Room is alerdy reserved for this year");
            }

            List<RenterRoom> renterRooms = await context.RenterRooms
                .Where(rr => rr.RoomId == model.RoomId)
                .OrderBy(rr => rr.StartDate)
                .ToListAsync();


            int i = 0;
            if (model.StartDate >= model.EndDate)
            {
                lst.Add("SartDate", "Start date must be before end date");
                return lst;
            }
            else if (renterRooms != null)
            {
                foreach (var r in renterRooms)
                {
                    DateTime nextStartDate = i < renterRooms.Count ? renterRooms[i].StartDate : new DateTime();

                    if (model.StartDate.ToShortDateString() == r.StartDate.ToShortDateString())
                    {
                        lst.Add("StartDate", "StartDate cannot be Start at the same date of an other contract");
                        return lst;
                    }
                    else if (model.EndDate.ToShortDateString() == r.EndDate.ToShortDateString())
                    {
                        lst.Add("EndDate", "EndDate cannot be End at the same date of an other contract");
                        return lst;
                    }
                    else if (model.StartDate > r.StartDate && model.EndDate < r.EndDate)
                    {
                        lst.Add("EndDate", "Dates Overlaping with Athor contract");
                        return lst;
                    }
                    else if (model.StartDate < r.StartDate)
                    {
                        if (model.EndDate >= r.StartDate)
                        {
                            lst.Add("EndDate", "Dates Overlaping with an athor contract");
                            return lst;
                        }
                    }
                    else if (model.StartDate > r.EndDate)
                    {
                        if (nextStartDate != DateTime.MinValue)
                        {
                            if (model.EndDate > nextStartDate && nextStartDate > model.StartDate)
                            {
                                lst.Add("EndDate", "Dates Overlaping with an athor contract");
                                return lst;
                            }
                        }
                    }
                    else if (model.StartDate > r.StartDate)
                    {
                        if (model.EndDate <= r.EndDate || model.StartDate <= model.EndDate)
                        {
                            lst.Add("EndDate", "Dates Overlaping with an athor contract");
                            return lst;
                        }
                    }
                    i++;
                }
            }
            return lst;
        }

        public async Task<Dictionary<string, string>> CheckoverlappingModalError(RenterRoomEditViewModel model)
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();

            int renterRoomId = model.RenterRoomId;

            bool isRoomReserved = await context.RenterRooms
                .AnyAsync(rr => rr.RoomId == model.RoomId && 
                rr.AcademicYearId == model.AcademicYearId &&
                rr.RenterRoomId != renterRoomId);

            if (isRoomReserved)
            {
                lst.Add("AcademicYearId", "Room is alerdy reserved for this year");
            }

            List<RenterRoom> renterRooms = await context.RenterRooms
                .Where(rr => rr.RoomId == model.RoomId && rr.RenterRoomId != renterRoomId)
                .OrderBy(rr => rr.StartDate)
                .ToListAsync();


            int i = 0;
            if (model.StartDate >= model.EndDate)
            {
                lst.Add("SartDate", "Start date must be before end date");
                return lst;
            }
            else if (renterRooms != null)
            {
                foreach (var r in renterRooms)
                {
                    DateTime nextStartDate = i < renterRooms.Count ? renterRooms[i].StartDate : new DateTime();

                    if (model.StartDate.ToShortDateString() == r.StartDate.ToShortDateString())
                    {
                        lst.Add("StartDate", "StartDate cannot be Start at the same date of an other contract");
                        return lst;
                    }
                    else if (model.EndDate.ToShortDateString() == r.EndDate.ToShortDateString())
                    {
                        lst.Add("EndDate", "EndDate cannot be End at the same date of an other contract");
                        return lst;
                    }
                    else if (model.StartDate > r.StartDate && model.EndDate < r.EndDate)
                    {
                        lst.Add("EndDate", "Dates Overlaping with Athor contract");
                        return lst;
                    }
                    else if (model.StartDate < r.StartDate)
                    {
                        if (model.EndDate >= r.StartDate)
                        {
                            lst.Add("EndDate", "Dates Overlaping with an athor contract");
                            return lst;
                        }
                    }
                    else if (model.StartDate > r.EndDate)
                    {
                        if (nextStartDate != DateTime.MinValue)
                        {
                            if (model.EndDate > nextStartDate && nextStartDate > model.StartDate)
                            {
                                lst.Add("EndDate", "Dates Overlaping with an athor contract");
                                return lst;
                            }
                        }
                    }
                    else if (model.StartDate > r.StartDate)
                    {
                        if (model.EndDate <= r.EndDate || model.StartDate <= model.EndDate)
                        {
                            lst.Add("EndDate", "Dates Overlaping with an athor contract");
                            return lst;
                        }
                    }
                    i++;
                }
            }
            return lst;
        }
    }
}
