using KotClassLibrary.Helpers;
using KotClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.ViewModels.RenterRoomVM
{
    public class RenterRoomDisplayViewModel : RenterRoom
    {
        public string FileBase64String => Contract != null ? FileHelper.GeneratePdf(Contract) : "";
    }
}
