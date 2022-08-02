using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Logging;
using SkyKotApp.Models;
using SkyKotApp.Services.General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISkyKotRepository skyKotRepository;
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(ISkyKotRepository skyKotRepository, IHtmlLocalizer<HomeController> localizer)
        {
            this.skyKotRepository = skyKotRepository;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            //ViewBag.Cloc = _localizer["HelloHome"];
            return View(await skyKotRepository.GetRoomsForHome());
        }
        [HttpPost]
        public IActionResult CultureManagement(string Culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(300) }
                );
            return LocalRedirect(returnUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
