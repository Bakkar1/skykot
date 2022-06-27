using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KotClassLibrary.Models;
using SkyKotApp.Data;
using Microsoft.AspNetCore.Authorization;
using SkyKotApp.Data.Default;
using System.Security.Claims;
using SkyKotApp.Services.General;

namespace SkyKotApp.Controllers
{
    [Authorize(Roles = Roles.Admin + ", " + Roles.Owner)]
    public class HouseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISkyKotRepository skyKotRepository;

        public HouseController(AppDbContext context, ISkyKotRepository skyKotRepository)
        {
            _context = context;
            this.skyKotRepository = skyKotRepository;
        }

        // GET: House
        public async Task<IActionResult> Index()
        {
            //var userRole = User.FindFirstValue(ClaimTypes.Role);
           
            return View(await skyKotRepository.GetHouses());
        }

        // GET: House/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if(skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the house
                if(!await skyKotRepository.IsOwnHouseAsync(id))
                {
                    return NotFound();
                }
            }

            var house = await _context.Houses
                .Include(h => h.Rooms)
                .Include(h => h.ZipCode)
                .FirstAsync(m => m.HouseId == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // GET: House/Create
        public IActionResult Create()
        {
            ViewData["ZipCodeId"] = new SelectList(_context.ZipCodes, "ZipCodeId", "City");
            return View();
        }

        // POST: House/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HouseId,Name,ZipCodeId,StreetName,HouseNumber")] House house)
        {
            if (ModelState.IsValid)
            {
                //var userName = User.FindFirstValue(ClaimTypes.Name);
                await skyKotRepository.AddHouse(house);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZipCodeId"] = new SelectList(_context.ZipCodes, "ZipCodeId", "City", house.ZipCodeId);
            return View(house);
        }

        // GET: House/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the house
                if (!await skyKotRepository.IsOwnHouseAsync(id))
                {
                    return NotFound();
                }
            }

            var house = await _context.Houses.FindAsync(id);
            if (house == null)
            {
                return NotFound();
            }
            ViewData["ZipCodeId"] = new SelectList(_context.ZipCodes, "ZipCodeId", "City", house.ZipCodeId);
            return View(house);
        }

        // POST: House/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HouseId,Name,ZipCodeId,StreetName,HouseNumber")] House house)
        {
            if (id != house.HouseId)
            {
                return NotFound();
            }

            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the house
                if (!await skyKotRepository.IsOwnHouseAsync(id))
                {
                    return NotFound();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(house);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseExists(house.HouseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZipCodeId"] = new SelectList(_context.ZipCodes, "ZipCodeId", "City", house.ZipCodeId);
            return View(house);
        }

        // GET: House/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the house
                if (!await skyKotRepository.IsOwnHouseAsync(id))
                {
                    return NotFound();
                }
            }

            var house = await _context.Houses
                .Include(h => h.ZipCode)
                .FirstOrDefaultAsync(m => m.HouseId == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // POST: House/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the house
                if (!await skyKotRepository.IsOwnHouseAsync(id))
                {
                    return NotFound();
                }
            }
            var house = await _context.Houses.FindAsync(id);
            _context.Houses.Remove(house);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HouseExists(int id)
        {
            return _context.Houses.Any(e => e.HouseId == id);
        }
    }
}
