using KotClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KotClassLibrary.ViewModels
{
    public class RegisterViewModel : CustomUser
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Profile Image (Optional)")]
        public List<IFormFile> Photo { get; set; }
    }
}
