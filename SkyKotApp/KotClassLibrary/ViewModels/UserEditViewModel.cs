using KotClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.ViewModels
{
    public class UserEditViewModel : CustomUser
    {
        public UserEditViewModel()
        {

        }
        public UserEditViewModel(CustomUser user)
        {
            HelperId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            ExistingPhotoPath = user.ProfileImage;
            IsAllowToUseTheApp = user.IsAllowToUseTheApp;
        }
        public IFormFile Photo { get; set; }
        public string RoleId { get; set; }
        public string HelperId { get; set; }
        public string ExistingPhotoPath { get; set; }
        public SelectList RolesSelectList { get; set; }
    }
}
