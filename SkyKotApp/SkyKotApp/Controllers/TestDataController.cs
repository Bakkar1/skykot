using KotClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyKotApp.Data;
using SkyKotApp.Data.Default;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class TestDataController : Controller
    {
        public TestDataController(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public async Task<IActionResult> AddPost()
        {
            var houses = Context.Houses.ToList();
            var images = Context.RoomImages.ToList();
            Random rnd = new Random();
            int rndHouseId = 0;
            for(int i = 0; i <= 20; i++)
            {
                rndHouseId = rnd.Next(houses.Count());
                Context.Rooms.Add(new Room()
                {
                    HouseId = houses[rndHouseId].HouseId,
                    Price = rnd.Next(100),
                    Description = "",
                    RoomNumber = rnd.Next(100),
                    RoomType = "Student",
                    MaxPeople = rnd.Next(100),
                    Period = rnd.Next(100),
                    AvailableFrom = DateTime.Now,
                    IsAvailable = true

                });
            }
            await Context.SaveChangesAsync();
            return RedirectToAction("Index", "Room");
        }
    }
}
