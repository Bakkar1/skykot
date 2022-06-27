using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KotClassLibrary.Models;
using SkyKotApp.Data;
using Microsoft.AspNetCore.Identity;

namespace SkyKotApp.Controllers
{
    public class RenterRoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<CustomUser> usermanage;

        public RenterRoomController(AppDbContext context, UserManager<CustomUser> usermanage)
        {
            _context = context;
            this.usermanage = usermanage;
        }

        // GET: RenterRoom
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RenterRooms.Include(r => r.AcademicYear).Include(r => r.CustomUser).Include(r => r.Room);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RenterRoom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renterRoom = await _context.RenterRooms
                .Include(r => r.AcademicYear)
                .Include(r => r.CustomUser)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RenterRoomId == id);
            if (renterRoom == null)
            {
                return NotFound();
            }

            return View(renterRoom);
        }

        // GET: RenterRoom/Create
        public IActionResult Create()
        {
            ViewData["AcademicYearId"] = new SelectList(_context.AcademicYears, "AcademicYearId", "StartDate");
            ViewData["Id"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomNumber");
            return View();
        }

        // POST: RenterRoom/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RenterRoomId,RoomId,Id,AcademicYearId,StartDate,EndDate")] RenterRoom renterRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(renterRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicYearId"] = new SelectList(_context.AcademicYears, "AcademicYearId", "AcademicYearId", renterRoom.AcademicYearId);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", renterRoom.Id);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomType", renterRoom.RoomId);
            return View(renterRoom);
        }

        // GET: RenterRoom/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renterRoom = await _context.RenterRooms.FindAsync(id);
            if (renterRoom == null)
            {
                return NotFound();
            }
            ViewData["AcademicYearId"] = new SelectList(_context.AcademicYears, "AcademicYearId", "AcademicYearId", renterRoom.AcademicYearId);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", renterRoom.Id);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomType", renterRoom.RoomId);
            return View(renterRoom);
        }

        // POST: RenterRoom/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RenterRoomId,RoomId,Id,AcademicYearId,StartDate,EndDate")] RenterRoom renterRoom)
        {
            if (id != renterRoom.RenterRoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(renterRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RenterRoomExists(renterRoom.RenterRoomId))
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
            ViewData["AcademicYearId"] = new SelectList(_context.AcademicYears, "AcademicYearId", "AcademicYearId", renterRoom.AcademicYearId);
            ViewData["Id"] = new SelectList(_context.Users, "Id", "Id", renterRoom.Id);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomType", renterRoom.RoomId);
            return View(renterRoom);
        }

        // GET: RenterRoom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renterRoom = await _context.RenterRooms
                .Include(r => r.AcademicYear)
                .Include(r => r.CustomUser)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RenterRoomId == id);
            if (renterRoom == null)
            {
                return NotFound();
            }

            return View(renterRoom);
        }

        // POST: RenterRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var renterRoom = await _context.RenterRooms.FindAsync(id);
            _context.RenterRooms.Remove(renterRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RenterRoomExists(int id)
        {
            return _context.RenterRooms.Any(e => e.RenterRoomId == id);
        }
    }
}
