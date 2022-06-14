using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KotClassLibrary.Models;
using SkyKotApp.Data;
using SkyKotApp.Data.Default;
using Microsoft.AspNetCore.Authorization;
using SkyKotApp.Services.General;

namespace SkyKotApp.Controllers
{
    [Authorize(Roles = Roles.Admin + ", " + Roles.Owner)]
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISkyKotRepository skyKotRepository;

        public RoomController(AppDbContext context, ISkyKotRepository skyKotRepository)
        {
            _context = context;
            this.skyKotRepository = skyKotRepository;
        }

        // GET: Room
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Rooms.Include(r => r.House);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Room/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the Room
                if (!await skyKotRepository.IsOwnRoom(id))
                {
                    return NotFound();
                }
            }

            var room = await _context.Rooms
                .Include(r => r.House)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Room/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["HouseId"] = new SelectList(await skyKotRepository.GetHouses(), "HouseId", "Name");
            return View();
        }

        // POST: Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,HouseId,RoomNumber,RoomType,MaxPeople,Period,AvailableFrom,IsAvailable")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HouseId"] = new SelectList(_context.Houses, "HouseId", "HouseNumber", room.HouseId);
            return View(room);
        }

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the Room
                if (!await skyKotRepository.IsOwnRoom(id))
                {
                    return NotFound();
                }
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["HouseId"] = new SelectList(_context.Houses, "HouseId", "HouseNumber", room.HouseId);
            return View(room);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,HouseId,RoomNumber,RoomType,MaxPeople,Period,AvailableFrom,IsAvailable")] Room room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }


            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the Room
                if (!await skyKotRepository.IsOwnRoom(id))
                {
                    return NotFound();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomId))
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
            ViewData["HouseId"] = new SelectList(_context.Houses, "HouseId", "HouseNumber", room.HouseId);
            return View(room);
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the Room
                if (!await skyKotRepository.IsOwnRoom(id))
                {
                    return NotFound();
                }
            }

            var room = await _context.Rooms
                .Include(r => r.House)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the Room
                if (!await skyKotRepository.IsOwnRoom(id))
                {
                    return NotFound();
                }
            }

            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}
