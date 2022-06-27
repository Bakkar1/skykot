using KotClassLibrary.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace SkyKotApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NotFoundEr(string categorie)
        {
            ErrorInfo errorInfo = new ErrorInfo()
            {
                Categorie = categorie
            };
            return View("NotFound", errorInfo);
        }
    }
}
