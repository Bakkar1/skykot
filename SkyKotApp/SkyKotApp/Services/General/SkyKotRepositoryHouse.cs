using KotClassLibrary.Models;
using KotClassLibrary.ViewModels.HouseVM;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<House> GetHouse(int houseId)
        {
            return await context.Houses
                .Include(h => h.Rooms)
                .Include(h => h.ZipCode)
                .Include(r => r.HouseSpecifications).ThenInclude(rs => rs.Specification)
                .Include(r => r.HouseExpenses).ThenInclude(re => re.Expence)
                .FirstAsync(m => m.HouseId == houseId);
        }

        public async Task<House> AddHouse(HouseCreateViewModel model)
        {
            House house = new House()
            {
                HouseId = model.HouseId,
                Name = model.Name,
                ZipCodeId = model.ZipCodeId,
                StreetName = model.StreetName,
                HouseNumber = model.HouseNumber,
                Description = model.Description,
                ContractRules = model.ContractRules
            };
            await context.Houses.AddAsync(house);
            await context.SaveChangesAsync();
            int houseId = house.HouseId;

            if (houseId != 0)
            {
                //add as own house
                string id = GetCurrentUserId();
                await context.UserHouses.AddAsync(new UserHouse
                {
                    Id = GetCurrentUserId(),
                    HouseId = houseId
                });
                //add spec
                if (model.HouseSpecificationsList != null)
                {
                    foreach (var hSpecification in model.HouseSpecificationsList)
                    {
                        if (hSpecification.IsAvailAble)
                        {
                            await context.HouseSpecifications.AddAsync(new HouseSpecification()
                            {
                                HouseId = houseId,
                                SpecificationId = hSpecification.SpecificationId,
                                IsAvailAble = string.IsNullOrWhiteSpace(hSpecification.WhereAvailAble) ? false : hSpecification.IsAvailAble,
                                WhereAvailAble = hSpecification.WhereAvailAble
                            });
                        }
                    }
                }

                //add expence
                if (model.HouseExpensesList != null)
                {
                    foreach (var hExpense in model.HouseExpensesList)
                    {
                        if (hExpense.Value != 0)
                        {
                            await context.HouseExpenses.AddAsync(new HouseExpense()
                            {
                                ExpenceId = hExpense.ExpenceId,
                                HouseId = houseId,
                                Value = hExpense.Value
                            });
                        }
                    }
                }

                await context.SaveChangesAsync();
            }
            return house;
        }

        public async Task<List<HouseSpecification>> GetHouseSpecificationsToCreate()
        {
            List<HouseSpecification> houseSpecifications = new List<HouseSpecification>();
            foreach (Specification specification in await GetSpecifications())
            {
                houseSpecifications.Add(new HouseSpecification()
                {
                    SpecificationId = specification.SpecificationId,
                    IsAvailAble = false,
                    Specification = new Specification()
                    {
                        Description = specification.Description
                    }
                });
            }
            return houseSpecifications;
        }
        public async Task<List<HouseExpense>> GetHouseExpenseToCreate()
        {
            List<HouseExpense> houseExpenses = new List<HouseExpense>();
            foreach (Expence expence in await GetExpences())
            {
                houseExpenses.Add(new HouseExpense()
                {
                    ExpenceId = expence.ExpenceId,
                    Expence = new Expence()
                    {
                        Description = expence.Description
                    }
                });
            }
            return houseExpenses;
        }

         public async Task<House> UpdateHouse(HouseEditViewModel model)
        {
            //update House
            House house = await context.Houses.FindAsync(model.HouseId);
            if(house != null)
            {
                house.Name = model.Name;
                house.ZipCodeId = model.ZipCodeId;
                house.StreetName = model.StreetName;
                house.HouseNumber = model.HouseNumber;
                house.Description = model.Description;
                house.ContractRules = model.ContractRules;
            }

            context.Update(house);

            int houseId = house.HouseId;

            // update House Specification
            if (model.HouseSpecificationsList != null)
            {
                foreach (var rSpecification in model.HouseSpecificationsList)
                {
                    if (rSpecification.HouseSpecificationId != 0)
                    {
                        HouseSpecification HouseSpec = await context.HouseSpecifications.FindAsync(rSpecification.HouseSpecificationId);
                        if (HouseSpec != null)
                        {
                            HouseSpec.IsAvailAble = string.IsNullOrWhiteSpace(rSpecification.WhereAvailAble) ? false : rSpecification.IsAvailAble;
                            HouseSpec.WhereAvailAble = rSpecification.WhereAvailAble;

                            context.HouseSpecifications.Update(HouseSpec);
                        }
                    }
                    else if (rSpecification.IsAvailAble)
                    {
                        await context.HouseSpecifications.AddAsync(new HouseSpecification()
                        {
                            HouseId = houseId,
                            SpecificationId = rSpecification.SpecificationId,
                            IsAvailAble = string.IsNullOrWhiteSpace(rSpecification.WhereAvailAble) ? false : rSpecification.IsAvailAble,
                            WhereAvailAble = rSpecification.WhereAvailAble
                        });
                    }
                }
            }
            // update House Expenses
            if (model.HouseExpensesList != null)
            {
                foreach (var rExpense in model.HouseExpensesList)
                {
                    if (rExpense.HouseExpenseId != 0)
                    {
                        HouseExpense HouseExpense = await context.HouseExpenses.FindAsync(rExpense.HouseExpenseId);
                        if (HouseExpense != null)
                        {
                            HouseExpense.Value = rExpense.Value;
                            context.HouseExpenses.Update(HouseExpense);
                        }
                    }
                    else
                    {
                        await context.HouseExpenses.AddAsync(new HouseExpense()
                        {
                            ExpenceId = rExpense.ExpenceId,
                            HouseId = houseId,
                            Value = rExpense.Value
                        });
                    }
                }
            }

            await context.SaveChangesAsync();
            return house;
        }

        public async Task<List<HouseSpecification>> GetHouseSpecificationsToEdit(ICollection<HouseSpecification> HouseSpecifications)
        {
            foreach (Specification specification in await GetSpecifications())
            {
                HouseSpecification HouseSpecification = HouseSpecifications.Where(rm => rm.Specification.SpecificationId == specification.SpecificationId).FirstOrDefault();
                if (HouseSpecification == null)
                {
                    HouseSpecifications.Add(new HouseSpecification()
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
            return HouseSpecifications.ToList();
        }
        public async Task<List<HouseExpense>> GetHouseExpensesToEdit(ICollection<HouseExpense> HouseExpenses)
        {
            foreach (Expence expence in await GetExpences())
            {
                HouseExpense HouseExpense = HouseExpenses.Where(re => re.ExpenceId == expence.ExpenceId).FirstOrDefault();
                if (HouseExpense == null)
                {
                    HouseExpenses.Add(new HouseExpense()
                    {
                        ExpenceId = expence.ExpenceId,
                        Expence = new Expence()
                        {
                            Description = expence.Description
                        }
                    });
                }
            }
            return HouseExpenses.ToList();
        }
        public async Task<SelectList> GetZipCodesSelect()
        {
            return new SelectList(await context.ZipCodes.ToListAsync(), nameof(ZipCode.ZipCodeId), nameof(ZipCode.City));
        }
    }
}
