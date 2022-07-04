using Microsoft.AspNetCore.Mvc;
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

        public HomeController(ISkyKotRepository skyKotRepository)
        {
            this.skyKotRepository = skyKotRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await skyKotRepository.GetRoomsForHome());
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
