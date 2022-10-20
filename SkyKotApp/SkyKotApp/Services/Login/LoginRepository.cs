using KotClassLibrary.Models;
using KotClassLibrary.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SkyKotApp.Data;
using SkyKotApp.Data.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkyKotApp.Services.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> singInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginRepository(AppDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> singInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.singInManager = singInManager;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task AddToUserRole(CustomUser identityUser)
        {
            await userManager.AddToRoleAsync(identityUser, Roles.NormalUser);
        }

        #region Login
        public IEnumerable<CustomUser> GetUsers()
        {
            return (IEnumerable<CustomUser>)context.Users.ToList();
        }
        public async Task<CustomUser> GetUserById(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }
        public async Task<CustomUser> GetUserAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public CustomUser GetUserByUserName(string userName)
        {
            return context.Users.Where(u => u.UserName == userName).FirstOrDefault() as CustomUser;
        }
        public async Task<SignInResult> SignInWithPassword(string userName, string password, bool remeberMe = false)
        {
            return await singInManager.PasswordSignInAsync(
                         userName, password, remeberMe, lockoutOnFailure: false);
        }
        public async Task SignIn(CustomUser user, bool IsPersistent)
        {
            await singInManager.SignInAsync(user, IsPersistent);
        }

        public async Task<IdentityResult> CreateUser(CustomUser user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task Logout()
        {
            await singInManager.SignOutAsync();
        }

        public async Task<bool> IsEmailConfirmed(CustomUser user)
        {
            return await userManager.IsEmailConfirmedAsync(user);
        }
        public async Task<IdentityResult> ConfirmEmail(CustomUser user, string token)
        {
            return await userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<bool> IsAdmin(CustomUser user)
        {
            return await userManager.IsInRoleAsync(user, Roles.Admin);
        }
        public async Task<bool> IsOwner(CustomUser user)
        {
            return await userManager.IsInRoleAsync(user, Roles.Owner);
        }
        #endregion

        #region External
        public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return await singInManager.GetExternalLoginInfoAsync();
        }

        public async Task<CustomUser> GetUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityResult> AddUser(CustomUser user)
        {
            return await userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> AddLogin(CustomUser user, ExternalLoginInfo externalLoginInfo)
        {
            return await userManager.AddLoginAsync(user, externalLoginInfo);
        }
        public string GetExternalName(ExternalLoginInfo externalLoginInfo)
        {
            return externalLoginInfo.Principal.FindFirst(ClaimTypes.Name).Value;
        }
        public string GetExternalNameByName(ExternalLoginInfo externalLoginInfo)
        {
            return externalLoginInfo.Principal.FindFirst("name").Value;
        }
        public string GetExternalEmail(ExternalLoginInfo externalLoginInfo)
        {
            return externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value;
        }
        public string GetExternalEmailByEmail(ExternalLoginInfo externalLoginInfo)
        {
            return externalLoginInfo.Principal.FindFirst("email").Value;
        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync(ExternalLoginInfo externalLoginInfo)
        {
            return await singInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, false);
        }
        public AuthenticationProperties GetEXtAuthProperties(string provider, string redirectUrl)
        {
            return singInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task<string> GenerateEmailConfirmationToken(CustomUser user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        #endregion

        #region Password
        public async Task<IdentityResult> ChangePassword(CustomUser user, string currentPassword, string newPassword)
        {
            return await userManager.ChangePasswordAsync(await GetCurrentUser(),
                    currentPassword, newPassword);
        }
        public async Task<CustomUser> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
           return await userManager.GetUserAsync(user);
        }
        public async Task RefreshSignIn(CustomUser user)
        {
            await singInManager.RefreshSignInAsync(user);
        }

        public async Task<string> GeneratePasswordResetToken(CustomUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(CustomUser user, string token, string newPassword)
        {
            return await userManager.ResetPasswordAsync(user, token, newPassword);
        }
        #endregion
    }
}
