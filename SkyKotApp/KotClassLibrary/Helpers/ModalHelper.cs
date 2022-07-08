using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Helpers
{
    public class ModalHelper
    {
        public static string GetCarouselId(int id)
        {
            return $"carousel-{id}";
        }
        public static string GetModalCarouselId(int id)
        {
            return $"modal-carousel-{id}";
        }
        public static string GetModalId(int id)
        {
            return $"Modal-{id}";
        }
    }
}
