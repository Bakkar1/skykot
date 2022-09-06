using KotClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.ViewModels.RenterRoomVM
{
    public class RenterRoomCreateViewModel : RenterRoom
    {
        public ICollection<AcademicYear> AcademicYears { get; set; }
        public ICollection<CustomUser> CustomUsers { get; set; }
        public ICollection<Room> Rooms { get; set; }

        public ICollection<House> Houses { get; set; }
    }
}
