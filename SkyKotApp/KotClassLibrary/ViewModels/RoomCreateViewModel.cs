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
    public class RoomCreateViewModel : Room
    {
        public List<IFormFile> Photos { get; set; }
        public List<string> ImagesPaths { get; set; }
        public SelectList HousesSelectList { get; set; }
    }
}
