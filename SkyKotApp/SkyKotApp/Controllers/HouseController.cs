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
using KotClassLibrary.ViewModels.HouseVM;

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
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
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

            var house = await skyKotRepository.GetHouse(id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        // GET: House/Create
        public async Task<IActionResult> CreateAsync()
        {
            HouseCreateViewModel model = new HouseCreateViewModel()
            {
                HouseSpecificationsList = await skyKotRepository.GetHouseSpecificationsToCreate(),
                HouseExpensesList = await skyKotRepository.GetHouseExpenseToCreate(),
            };
            ViewData["ZipCodeId"] = new SelectList(_context.ZipCodes, "ZipCodeId", "City");
            return View(model);
        }

        // POST: House/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("HouseId,Name,ZipCodeId,StreetName,HouseNumber,Description")]
        public async Task<IActionResult> Create(HouseCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await skyKotRepository.AddHouse(model);
                return RedirectToAction(nameof(Index));
            }
            model.HouseSpecificationsList = await skyKotRepository.GetHouseSpecificationsToCreate();
            model.HouseExpensesList = await skyKotRepository.GetHouseExpenseToCreate();
            ViewData["ZipCodeId"] = new SelectList(_context.ZipCodes, "ZipCodeId", "City", model.ZipCodeId);
            return View(model);
        }

        // GET: House/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
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

            var house = await skyKotRepository.GetHouse(id);
            if (house == null)
            {
                return NotFound();
            }

            HouseEditViewModel model = new HouseEditViewModel(house)
            {
                HouseSpecificationsList = await skyKotRepository.GetHouseSpecificationsToEdit(house.HouseSpecifications),
                HouseExpensesList = await skyKotRepository.GetHouseExpensesToEdit(house.HouseExpenses),
                ZipCodesSelectList = await skyKotRepository.GetZipCodesSelect()
            };
            return View(model);
        }

        // POST: House/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("HouseId,Name,ZipCodeId,StreetName,HouseNumber,Description")]
        public async Task<IActionResult> Edit(int id, HouseEditViewModel model)
        {
            if (id != model.HouseId)
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
                    await skyKotRepository.UpdateHouse(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseExists(model.HouseId))
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
            model.HouseSpecificationsList = await skyKotRepository.GetHouseSpecificationsToEdit(model.HouseSpecifications);
            model.HouseExpensesList = await skyKotRepository.GetHouseExpensesToEdit(model.HouseExpenses);
            model.ZipCodesSelectList = await skyKotRepository.GetZipCodesSelect();
            return View(model);
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
