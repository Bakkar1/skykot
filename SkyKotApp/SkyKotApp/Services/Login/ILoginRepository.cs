using KotClassLibrary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Services.Login
{
    public interface ILoginRepository
    {
        #region Account
        IEnumerable<CustomUser> GetUsers();
        Task<CustomUser> GetUserAsync(string email);
        Task<SignInResult> SignInWithPassword(string userName, string password, bool remeberMe = false);
        Task SignIn(CustomUser user, bool IsPersistent);
        Task<IdentityResult> CreateUser(CustomUser user, string password);
        Task Logout();
        CustomUser GetUserByUserName(string userName);
        Task<bool> IsEmailConfirmed(CustomUser user);
        Task<IdentityResult> ConfirmEmail(CustomUser user, string token);
        Task<CustomUser> GetUserById(string userId);
        #endregion
        #region External Login
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<CustomUser> GetUserByEmail(string email);
        string GetExternalName(ExternalLoginInfo externalLoginInfo);
        string GetExternalNameByName(ExternalLoginInfo externalLoginInfo);
        string GetExternalEmail(ExternalLoginInfo externalLoginInfo);
        string GetExternalEmailByEmail(ExternalLoginInfo externalLoginInfo);
        Task<Microsoft.AspNetCore.Identity.SignInResult> ExternalLoginSignInAsync(ExternalLoginInfo externalLoginInfo);
        Task<IdentityResult> AddLogin(CustomUser user, ExternalLoginInfo externalLoginInfo);
        AuthenticationProperties GetEXtAuthProperties(string provider, string redirectUrl);
        Task<IdentityResult> AddUser(CustomUser user);
        Task<string> GenerateEmailConfirmationToken(CustomUser user);
        #endregion
    }
}
