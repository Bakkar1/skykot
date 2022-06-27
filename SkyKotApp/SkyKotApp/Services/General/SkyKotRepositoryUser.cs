using KotClassLibrary.Helpers;
using KotClassLibrary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkyKotApp.Data;
using SkyKotApp.Data.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkyKotApp.Services.General
{
    public partial class SkyKotPartialRepository : ISkyKotRepository
    {
        private readonly AppDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> singInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment hostEnvironment;

        public SkyKotPartialRepository(AppDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> singInManager,
            IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.singInManager = singInManager;
            this._httpContextAccessor = httpContextAccessor;
            this.hostEnvironment = hostEnvironment;
        }
        public async Task<ICollection<CustomUser>> GetCustomUsers()
        {
            return await context.Users.ToListAsync();
        }
        public async Task<ICollection<CustomUser>> GetOwnCustomUsers()
        {
            return await context
                .Users
                .Where(u => u.OwnerId == GetCurrentUserId())
                .ToListAsync();
        }

        public async Task<CustomUser> GetUser(string id)
        {
            return await context.Users.FindAsync(id);
        }
        public async Task<CustomUser> UpdateUser(CustomUser user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }
        public async Task<CustomUser> DeleteUser(string id)
        {
            CustomUser user = await context.Users.FindAsync(id);

            DeleteOldRole(user.Id);
            //delete profile photo
            if (!string.IsNullOrEmpty(user.ProfileImage))
            {
                PhotoHelper.DeleteProfilePhoto(hostEnvironment, user.ProfileImage);
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }
        public async Task<CustomUser> GetUserByName(string userName)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public string GetCurrentUserRole()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        public async Task AddToRenterRole(CustomUser identityUser)
        {
            await userManager.AddToRoleAsync(identityUser, Roles.Renter);
        }
        public async Task<SelectList> GetHousesSelectList()
        {
            return new SelectList(await GetHouses(), nameof(House.HouseId), nameof(House.Name));
        }

    }
}
