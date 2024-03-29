﻿using KotClassLibrary.Helpers;
using KotClassLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Data.Default
{
    public class SeedData
    {
        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<AppDbContext>();

            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            UserManager<CustomUser> userManager = app.ApplicationServices
               .CreateScope()
               .ServiceProvider
               .GetRequiredService<UserManager<CustomUser>>();

            await CreateRolesAsync(context, roleManager);
            await CreateIdentityRecordAsync(userManager);
            await FullInitialData(context);
        }
        private static async Task CreateRolesAsync(AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            if (!context.Roles.Any())
            {
                await CreateRoleAsync(roleManager, Roles.NormalUser);
                await CreateRoleAsync(roleManager, Roles.Admin);
                await CreateRoleAsync(roleManager, Roles.Owner);
                await CreateRoleAsync(roleManager, Roles.Renter);
            }
        }
        private static async Task CreateRoleAsync(RoleManager<IdentityRole> roleManager, string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                IdentityRole identityRole = new IdentityRole(role);
                await roleManager.CreateAsync(identityRole);
            }
        }
        private static async Task CreateIdentityRecordAsync(UserManager<CustomUser> userManager)
        {
            var email = "mbarkbakkar1@gmail.com";
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var pwd = "Mbark123!";
                var identityUser = new CustomUser() { 
                    Email = email, 
                    UserName = "Mbar_Bakkar1",
                    FirstName = "Mbark",
                    LastName = "Bakkar"
                };
                await userManager.CreateAsync(identityUser, pwd);
                await userManager.AddToRoleAsync(identityUser, Roles.Admin);
            }

            var emailOwner = "mbarkbakkar1@outlook.com";
            if (await userManager.FindByEmailAsync(emailOwner) == null)
            {
                var pwd = "Mbark123!";
                var identityUser = new CustomUser()
                {
                    Email = emailOwner,
                    UserName = "Mbar_Bakkar22",
                    FirstName = "Mbark owner",
                    LastName = "Bakkar"
                };
                await userManager.CreateAsync(identityUser, pwd);
                await userManager.AddToRoleAsync(identityUser, Roles.Owner);
            }
        }
        public static async Task FullInitialData(AppDbContext context)
        {
            if (!context.Countries.Any())
            {
                Country belgium = new Country() { Name = "Belgium" };
                await context.Countries.AddAsync(belgium);
                await context.SaveChangesAsync();
                await context.ZipCodes.AddRangeAsync(
                    new ZipCode()
                    {
                        Code = "3500",
                        City = "Hasselt",
                        CountryId = belgium.CountryId
                    },
                    new ZipCode()
                    {
                        Code = "3590",
                        City = "Diepenbeek",
                        CountryId = belgium.CountryId
                    },
                    new ZipCode()
                    {
                        Code = "3530",
                        City = "Houthalen",
                        CountryId = belgium.CountryId
                    }
                    );
                //await context.SaveChangesAsync();
            }
            if (!context.AcademicYears.Any())
            {
                DateTime date = new DateTime(DateTime.Now.Year, 9, 20);
                for(int i = 0; i < 10; i++)
                {
                    await context.AcademicYears.AddRangeAsync(new AcademicYear()
                        {
                            StartDate = date
                        });
                    date = date.AddYears(1);
                }
                //await context.SaveChangesAsync();
            }
            if (!context.Expences.Any())
            {
                await context.Expences.AddRangeAsync(
                        new Expence()
                        {
                            Description = "Taks"
                        },
                        new Expence()
                        {
                            Description = "Water"
                        },
                        new Expence()
                        {
                            Description = "Heating In The Room"
                        },
                        new Expence()
                        {
                            Description = "Electricity"
                        },
                        new Expence()
                        {
                            Description = "Heating In The Building"
                        }
                    );
            }
            if (!context.Expences.Any())
            {
                await context.Specifications.AddRangeAsync(
                        new Specification()
                        {
                            Description = "Tuin"
                        },
                        new Specification()
                        {
                            Description = "Parking"
                        },
                        new Specification()
                        {
                            Description = "Wifi"
                        },
                        new Specification()
                        {
                            Description = "Douche"
                        },
                        new Specification()
                        {
                            Description = "Bemeubeld"
                        }
                    );
            }
            await context.SaveChangesAsync();
        }
    }
}
