using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Helpers
{
    public class RoomType
    {
        public const string Student = "Student";
        public const string Studio = "Studio";
        public const string Duplex = "Duplex";
        public static SelectList GetTypes()
        {
            List<string> types = new List<string>()
            {
                Student,
                Studio,
                Duplex
            };
            return new SelectList(types);
        }
    }
}
