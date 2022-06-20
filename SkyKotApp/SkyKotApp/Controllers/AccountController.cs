using KotClassLibrary.Helpers;
using KotClassLibrary.Models;
using KotClassLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkyKotApp.Services.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkyKotApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginRepository loginRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public AccountController(
             ILoginRepository loginRepository,
             IWebHostEnvironment hostingEnvironment)
        {
            this.loginRepository = loginRepository;
            this.hostingEnvironment = hostingEnvironment;
        }
        [Authorize]
        public IActionResult Profile()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            return View(loginRepository.GetUserByUserName(userName));
        }

        #region register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new CustomUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = $"{model.LastName}_{model.FirstName}_{Guid.NewGuid()}" // make userNmae Unique
                };
                identityUser.ProfileImage = ProcessUploadedFile(model);

                var result = await loginRepository.CreateUser(identityUser, model.Password);

                if (result.Succeeded)
                {
                    var token = await loginRepository.GenerateEmailConfirmationToken(identityUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { userId = identityUser.Id, token = token }, Request.Scheme);

                    try
                    {
                        EmailHelper.SendConfirmationEmail(identityUser.Email, confirmationLink);
                    }
                    catch (Exception)
                    {
                        //do nothing
                    }

                    ViewBag.ErrorTitle = "Registration successful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                            "email, by clicking on the confirmation link we have emailed you";
                    return View("AccessDenied");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        #endregion
        #region LogOut
        public async Task<IActionResult> Logout()
        {
            await loginRepository.Logout();
            return View("LogoutCompleted");
        }
        #endregion
        #region login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityUser = await loginRepository.GetUserAsync(model.Email);

                if (identityUser != null)
                {

                    if (await loginRepository.IsAdmin(identityUser) /*|| await loginRepository.IsEmailConfirmed(identityUser)*/)
                    {
                        var userName = identityUser.UserName;
                        var result = await loginRepository.SignInWithPassword(
                             userName, model.Password, model.RememberMe);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please Valid Your Email");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Login Attempt");

            return View(model);
        }
        #endregion
        #region ExternalLogin
        public IActionResult FacebookLogin()
        {
            string redirectUrl = Url.Action("ExternalLoginResponse", "Account");
            var properties = loginRepository.GetEXtAuthProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("ExternalLoginResponse", "Account");
            var properties = loginRepository.GetEXtAuthProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }
        public async Task<IActionResult> ExternalLoginResponse()
        {
            ExternalLoginInfo externalLoginInfo = await loginRepository.GetExternalLoginInfoAsync();

            if (externalLoginInfo == null)
            {
                ModelState.AddModelError("", "No authentication was provided");
                return RedirectToAction(nameof(Login));
            }

            string userName;
            string externalName;
            string email;
            if (externalLoginInfo.Principal.FindFirst(ClaimTypes.Name) != null)
            {
                externalName = loginRepository.GetExternalName(externalLoginInfo);
            }
            else
            {
                externalName = loginRepository.GetExternalNameByName(externalLoginInfo);
            }

            if (externalLoginInfo.Principal.FindFirst(ClaimTypes.Email) != null)
            {
                email = loginRepository.GetExternalEmail(externalLoginInfo);
            }
            else
            {
                email = loginRepository.GetExternalEmailByEmail(externalLoginInfo);
            }

            userName = externalName.Replace(" ", "");

            userName = $"{userName}_{externalLoginInfo.LoginProvider}_{externalLoginInfo.ProviderKey}"; // to make user unique

            UserInfoModel userInfoModel = new UserInfoModel
            {
                UserName = userName,
                FirstName = externalName.Substring(0, externalName.IndexOf(' ')),
                LastName = externalName.Substring(externalName.IndexOf(' ') + 1),
                Email = email
            };


            var result = await loginRepository.ExternalLoginSignInAsync(externalLoginInfo);
            if (result.Succeeded)
            {
                return View(userInfoModel);
            }

            CustomUser existUser = await loginRepository.GetUserByEmail(email);

            if (existUser != null)
            {
                var identityRe = await loginRepository.AddLogin(existUser, externalLoginInfo);
                if (identityRe.Succeeded)
                {
                    await loginRepository.SignIn(existUser, false);
                    return View(userInfoModel);
                }
                return View("AccessDenied", "Registration of the new login method failed");
            }

            CustomUser user = new CustomUser()
            {
                UserName = userName,
                FirstName = userInfoModel.FirstName,
                LastName = userInfoModel.LastName,
                Email = userInfoModel.Email
            };


            IdentityResult identityResult = await loginRepository.AddUser(user);

            if (identityResult.Succeeded)
            {
                identityResult = await loginRepository.AddLogin(user, externalLoginInfo);
                if (identityResult.Succeeded)
                {
                    await loginRepository.SignIn(user, false);
                    return View(userInfoModel);
                }
                return View("AccessDenied", "Registration of the new login method failed");
            }

            return View("AccessDenied", "Registration of the new User is failed");
        }
        #endregion
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await loginRepository.GetUserById(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await loginRepository.ConfirmEmail(user, token);
            if (result.Succeeded)
            {
                await loginRepository.SignIn(user, false);
                return RedirectToAction("index", "Home");
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
        private string ProcessUploadedFile(RegisterViewModel model)
        {
            string uniqueFileName = "";
            if (model.Photo != null && model.Photo.Count > 0)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images/Profile");

                IFormFile photo = model.Photo[0];

                uniqueFileName = Guid.NewGuid().ToString() + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fs);
                }
            }
            return uniqueFileName;
        }
    }
}
