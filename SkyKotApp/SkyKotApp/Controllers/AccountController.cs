using KotClassLibrary.Helpers;
using KotClassLibrary.Models;
using KotClassLibrary.ViewModels;
using KotClassLibrary.ViewModels.Register;
using KotClassLibrary.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkyKotApp.Data.Default;
using SkyKotApp.Services.General;
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
        private readonly ISkyKotRepository skyKotRepository;

        public AccountController(
             ILoginRepository loginRepository,
             IWebHostEnvironment hostingEnvironment,
             ISkyKotRepository skyKotRepository)
        {
            this.loginRepository = loginRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.skyKotRepository = skyKotRepository;
        }
        #region Profile
        [Authorize]
        public IActionResult Profile()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            return View(loginRepository.GetUserByUserName(userName));
        }
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            string id = skyKotRepository.GetCurrentUserId();
            if (string.IsNullOrEmpty(id))
            {
                return RedirecToNotFound();
            }

            CustomUser user = await skyKotRepository.GetUser(id);
            if (user == null)
            {
                return RedirecToNotFound();
            }
            UserEditViewModel model = new UserEditViewModel(user);
            model.RoleId = skyKotRepository.GetRoleName(user.Id);
            if (skyKotRepository.GetCurrentUserRole() == Roles.Admin)
            {
                model.RolesSelectList = skyKotRepository.GetRoles();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = null;
                try
                {
                    string id = skyKotRepository.GetCurrentUserId();
                    if(string.IsNullOrEmpty(id))
                    {
                        return RedirecToNotFound();
                    }
                    user = await skyKotRepository.GetUser(id);

                    if (await skyKotRepository.IsAlredyEmailExist(model.Email, id))
                    {
                        ModelState.AddModelError("Email", "Email Alredy Exit");
                        return View(model);
                    }

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;

                    if (model.Photo == null)
                    {
                        if (!string.IsNullOrEmpty(model.ExistingPhotoPath))
                        {
                            model.ProfileImage = model.ExistingPhotoPath;
                        }
                    }
                    else
                    {
                        user.ProfileImage = PhotoHelper.UploadProfilePhoto(hostingEnvironment, model.Photo);
                        PhotoHelper.DeleteProfilePhoto(hostingEnvironment, model.ExistingPhotoPath);
                    }
                    await skyKotRepository.UpdateUser(user);

                    if (skyKotRepository.GetCurrentUserRole() == Roles.Admin)
                    {
                        //update role
                        await skyKotRepository.UpdateRole(user, model.RoleId);
                    }
                    return RedirectToAction("Profile");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (user == null)
                    {
                        return RedirecToNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(model);
        }


        #endregion

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
                var identityUser = new CustomUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = $"{model.LastName}_{model.FirstName}_{Guid.NewGuid()}", // make userNmae Unique
                    ProfileImage = PhotoHelper.UploadProfilePhoto(hostingEnvironment, model.Photo)
                };

                var result = await loginRepository.CreateUser(identityUser, model.Password);

                if (result.Succeeded)
                {
                    await loginRepository.AddToUserRole(identityUser);
                    var token = await loginRepository.GenerateEmailConfirmationToken(identityUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { userId = identityUser.Id, token = token }, Request.Scheme);

                    AfterRegisterViewModel afterRegister = new AfterRegisterViewModel()
                        {
                            Title = "Registration successful",
                            Message = $"Before you can Login, please confirm your email, by clicking on the confirmation link we have emailed you, Email was sended to {identityUser.Email}"
                    };
                    try
                    {
                        EmailHelper.SendConfirmationEmail(identityUser.Email, confirmationLink);
                    }
                    catch (Exception)
                    {
                        afterRegister.IsEmailSended = false;
                    }

                    return RedirectToAction("EmailConfirmation",afterRegister);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        public IActionResult EmailConfirmation(AfterRegisterViewModel afterRegister)
        {
            if(afterRegister == null)
            {
                afterRegister.Title = "";
                afterRegister.Message = "";
            }
            return View(afterRegister);
        }
        #endregion

        #region LogOut
        public async Task<IActionResult> Logout()
        {
            await loginRepository.Logout();
            return RedirectToAction("LogoutCompleted");
        }
        public IActionResult LogoutCompleted()
        {
            return View();
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

                    if (true /*await loginRepository.IsAdmin(identityUser) || await loginRepository.IsEmailConfirmed(identityUser)*/)
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
                        AfterRegisterViewModel afterRegister = new AfterRegisterViewModel()
                        {
                            Title = "Login Faild",
                            Email = identityUser.Email,
                            Message = $"Before you can Login, please confirm your email, by clicking on the confirmation link we have emailed you, Email was sended to {identityUser.Email}"
                        };
                        return RedirectToAction("EmailConfirmation", afterRegister);
                    }
                }
            }
            ModelState.AddModelError("", "Password or Email is not Correct");

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
                return RedirectToAction("Index","Home");
            }

            CustomUser existUser = await loginRepository.GetUserByEmail(email);

            if (existUser != null)
            {
                var identityRe = await loginRepository.AddLogin(existUser, externalLoginInfo);
                if (identityRe.Succeeded)
                {
                    await loginRepository.SignIn(existUser, false);
                    return RedirectToAction("Index", "Home");
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
                    await loginRepository.AddToUserRole(user);

                    await loginRepository.SignIn(user, false);
                    //return View(userInfoModel);
                    return RedirectToAction("Index", "Home");
                }
                return View("AccessDenied", "Registration of the new login method failed");
            }

            return View("AccessDenied", "Registration of the new User is failed");
        }
        #endregion

        #region AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

        #region EmailConfirmation
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
                //await loginRepository.SignIn(user, false);
                return View("EmailConfirmed");
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
        public async Task<IActionResult> SendConfirmationEmail(string email)
        {
            if(email == null)
            {
                return NotFound();
            }
            CustomUser identityUser = await loginRepository.GetUserByEmail(email);
            if (identityUser == null)
            {
                return NotFound();
            }
            var token = await loginRepository.GenerateEmailConfirmationToken(identityUser);
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
            new { userId = identityUser.Id, token = token }, Request.Scheme);

            AfterRegisterViewModel afterRegister = new AfterRegisterViewModel()
            {
                Title = "Confrmation Email Sended",
                Message = $"Confirmation Email was sended to {identityUser.Email}"
            };
            try
            {
                EmailHelper.SendConfirmationEmail(identityUser.Email, confirmationLink);
            }
            catch (Exception)
            {
                afterRegister.IsEmailSended = false;
            }

            return RedirectToAction("EmailConfirmation", afterRegister);
        }
        #endregion

        #region ChangePassword
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await loginRepository.GetCurrentUser();
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                // ChangePasswordAsync changes the user password
                var result = await loginRepository.ChangePassword(user,
                    model.CurrentPassword, model.NewPassword);

                // The new password did not meet the complexity rules or
                // the current password is incorrect. Add these errors to
                // the ModelState and rerender ChangePassword view
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                // Upon successfully changing the password refresh sign-in cookie
                await loginRepository.RefreshSignIn(user);
                return View("ChangePasswordConfirmation");
            }

            return View(model);
        }
        #endregion

        #region ForgotPassword

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await loginRepository.GetUserAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null && await loginRepository.IsEmailConfirmed(user))
                {
                    // Generate the reset password token
                    var token = await loginRepository.GeneratePasswordResetToken(user);

                    // Build the password reset link
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);

                    try
                    {
                        EmailHelper.SendRestPasswordlink(model.Email, passwordResetLink);
                    }
                    catch (Exception)
                    {
                        
                    }


                    // Log the password reset link
                    //logger.Log(LogLevel.Warning, passwordResetLink);

                    // Send the user to Forgot Password Confirmation view
                    return View("ForgotPasswordConfirmation");
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await loginRepository.GetUserAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await loginRepository.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }
        #endregion

        private RedirectToActionResult RedirecToNotFound()
        {
            return RedirectToAction(NotFoundIdInfo.ActionName, NotFoundIdInfo.ControllerName, new { categorie = "User" });
        }
    }
}
