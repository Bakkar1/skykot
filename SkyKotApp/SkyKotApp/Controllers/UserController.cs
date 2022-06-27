using KotClassLibrary.Helpers;
using KotClassLibrary.Models;
using KotClassLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkyKotApp.Data.Default;
using SkyKotApp.Services.General;
using SkyKotApp.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkyKotApp.Controllers
{
    [Authorize(Roles = Roles.Admin + ", " + Roles.Owner)]
    public class UserController : Controller
    {
        private readonly ISkyKotRepository skyKotRepository;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly ILoginRepository loginRepository;

        public UserController(
            ISkyKotRepository skyKotRepository,
            IWebHostEnvironment hostEnvironment,
            ILoginRepository loginRepository)
        {
            this.skyKotRepository = skyKotRepository;
            this.hostEnvironment = hostEnvironment;
            this.loginRepository = loginRepository;
        }
        public async Task<IActionResult> Index()
        {
            string currentUserRole = skyKotRepository.GetCurrentUserRole();
            if (currentUserRole == Roles.Admin)
            {
                return View(await skyKotRepository.GetCustomUsers());
            }
            else if(currentUserRole == Roles.Owner)
            {
                return View(await skyKotRepository.GetOwnCustomUsers());
            }
            return NotFound();
            
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return RedirecToNotFound();
            }

            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check is owner
                if (!await skyKotRepository.IsUserOwner(id))
                {
                    return RedirecToNotFound();
                }
            }

            var user = await skyKotRepository.GetUser(id);
            if (user == null)
            {
                return RedirecToNotFound();
            }

            return View(user);
        }

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
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
                identityUser.ProfileImage = PhotoHelper.UploadProfilePhoto(hostEnvironment, model.Photo);

                if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
                {
                    identityUser.OwnerId = skyKotRepository.GetCurrentUserId();
                }

                var result = await loginRepository.CreateUser(identityUser, model.Password);

                if (result.Succeeded)
                {
                    if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
                    {
                        // add to renter role
                        await skyKotRepository.AddToRenterRole(identityUser);
                    }
                    var token = await loginRepository.GenerateEmailConfirmationToken(identityUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { userId = identityUser.Id, token = token }, Request.Scheme);

                    try
                    {
                        EmailHelper.SendInvatationEmail(identityUser.Email, confirmationLink);
                    }
                    catch (Exception)
                    {
                        //do nothing
                    }
                    //if current user is owner
                    if(skyKotRepository.GetCurrentUserRole() == Roles.Owner)
                    {
                        //add as role member
                        await skyKotRepository.AddToRenterRole(identityUser);
                    }
                    
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return RedirecToNotFound();
            }
            if(skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check is owner
                if (!await skyKotRepository.IsUserOwner(id))
                {
                    return RedirecToNotFound();
                }
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

        // POST: Gebruiker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        {
            if (id != model.HelperId)
            {
                return RedirecToNotFound();
            }

            if (ModelState.IsValid)
            {
                if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
                {
                    //check is owner
                    if (!await skyKotRepository.IsUserOwner(id))
                    {
                        return RedirecToNotFound();
                    }
                }
                CustomUser user = null;
                try
                {
                    user = await skyKotRepository.GetUser(model.HelperId);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;

                    user.ProfileImage = PhotoHelper.UploadProfilePhoto(hostEnvironment, model.Photo);

                    PhotoHelper.DeleteProfilePhoto(hostEnvironment, model.ExistingPhotoPath);

                    await skyKotRepository.UpdateUser(user);
                    if (skyKotRepository.GetCurrentUserRole() == Roles.Admin)
                    {
                        //update role
                        await skyKotRepository.UpdateRole(user, model.RoleId);
                    }
                    return RedirectToAction("Index");
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
        #region Delete
        // GET: Gebruiker/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirecToNotFound();
            }
            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check is owner
                if (!await skyKotRepository.IsUserOwner(id))
                {
                    return RedirecToNotFound();
                }
            }

            var gebruiker = await skyKotRepository.GetUser(id);
            if (gebruiker == null)
            {
                return RedirecToNotFound();
            }

            return View(gebruiker);
        }

        // POST: Gebruiker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check is owner
                if (!await skyKotRepository.IsUserOwner(id))
                {
                    return RedirecToNotFound();
                }
            }
            await skyKotRepository.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        private RedirectToActionResult RedirecToNotFound()
        {
            return RedirectToAction(NotFoundIdInfo.ActionName, NotFoundIdInfo.ControllerName, new { categorie = "User" });
        }
    }
}
