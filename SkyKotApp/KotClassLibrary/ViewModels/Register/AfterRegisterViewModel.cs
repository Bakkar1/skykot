using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.ViewModels.Register
{
    public class AfterRegisterViewModel
    {
        public AfterRegisterViewModel()
        {

        }
        public AfterRegisterViewModel(string title)
        {
            Title = title;
        }
        public string Title { get; set; }
        public string Email { get; set; }
        public bool IsEmailSended { get; set; } = true;
        public string Message { get; set; }

    }
}
