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
using SkyKotApp.Services.General;
using SkyKotApp.Data.Default;
using KotClassLibrary.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace SkyKotApp.Controllers
{
    [Authorize(Roles = Roles.Admin + ", " + Roles.Owner)]
    public class RenterRoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<CustomUser> usermanage;
        private readonly ISkyKotRepository skyKotRepository;

        public RenterRoomController(AppDbContext context, UserManager<CustomUser> usermanage, ISkyKotRepository skyKotRepository)
        {
            _context = context;
            this.usermanage = usermanage;
            this.skyKotRepository = skyKotRepository;
        }

        // GET: RenterRoom
        public async Task<IActionResult> Index()
        {
            return View(await skyKotRepository.GetRenterRooms());
        }

        // GET: RenterRoom/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return RedirecToNotFound();
            }
            
            if (skyKotRepository.GetCurrentUserRole() != Roles.Admin)
            {
                //check if is own renter Room
                if (!await skyKotRepository.IsOwnRenterRoom(id))
                {
                    return RedirecToNotFound();
                }
            }
            var renterRoom = await skyKotRepository.GetRenterRoom(id);
            if (renterRoom == null)
            {
                return RedirecToNotFound();
            }

            return View(renterRoom);
        }

        // GET: RenterRoom/Create
        public async Task<IActionResult> Create()
        {
            return View(await skyKotRepository.GetRenterRoomCreateViewModel());
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
                if (skyKotRepository.GetCurrentUserRole() != Roles.Admin)
                {
                    //check if is owner of the Room
                    if (!await skyKotRepository.IsOwnRoom(renterRoom.RoomId) && !await skyKotRepository.IsUserOwner(renterRoom.Id))
                    {
                        return RedirecToNotFound();
                    }
                }
                Room room = await skyKotRepository.GetRoom(renterRoom.RoomId);
                var toPay = room.RoomExpenses.Sum(re => re.Value) + room.Price;
                renterRoom.AmountToPay = toPay;

                _context.Add(renterRoom);
                await _context.SaveChangesAsync();
                
                int renterRoomId = renterRoom.RenterRoomId;

                DateTime startDate = renterRoom.StartDate;
                DateTime endDate = renterRoom.EndDate;

                while (startDate <= endDate)
                {
                    _context.RenterContracts.Add(new RenterContract()
                    {
                        StartDate = startDate,
                        RenterRoomId = renterRoomId,
                        IsPayed = false
                    });
                    startDate = startDate.AddMonths(1);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(renterRoom);
        }

        // GET: RenterRoom/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return RedirecToNotFound();
            }
            if (skyKotRepository.GetCurrentUserRole() != Roles.Admin)
            {
                //check if is own renter Room
                if (!await skyKotRepository.IsOwnRenterRoom(id))
                {
                    return RedirecToNotFound();
                }
            }
            var renterRoom = await _context.RenterRooms.FindAsync(id);
            if (renterRoom == null)
            {
                return RedirecToNotFound();
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
                return RedirecToNotFound();
            }

            if (ModelState.IsValid)
            {
                if (skyKotRepository.GetCurrentUserRole() != Roles.Admin)
                {
                    //check if is own renter Room
                    if (!await skyKotRepository.IsOwnRenterRoom(id))
                    {
                        return RedirecToNotFound();
                    }
                }
                try
                {
                    _context.Update(renterRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RenterRoomExists(renterRoom.RenterRoomId))
                    {
                        return RedirecToNotFound();
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return RedirecToNotFound();
            }

            if (skyKotRepository.GetCurrentUserRole() != Roles.Admin)
            {
                //check if is own renter Room
                if (!await skyKotRepository.IsOwnRenterRoom(id))
                {
                    return RedirecToNotFound();
                }
            }

            var renterRoom = await _context.RenterRooms
                .Include(r => r.AcademicYear)
                .Include(r => r.CustomUser)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RenterRoomId == id);
            if (renterRoom == null)
            {
                return RedirecToNotFound();
            }

            return View(renterRoom);
        }

        // POST: RenterRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (skyKotRepository.GetCurrentUserRole() != Roles.Admin)
            {
                //check if is own renter Room
                if (!await skyKotRepository.IsOwnRenterRoom(id))
                {
                    return RedirecToNotFound();
                }
            }

            var renterRoom = await _context.RenterRooms.FindAsync(id);
            _context.RenterRooms.Remove(renterRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RenterRoomExists(int id)
        {
            return _context.RenterRooms.Any(e => e.RenterRoomId == id);
        }

        private RedirectToActionResult RedirecToNotFound()
        {
            return RedirectToAction(NotFoundIdInfo.ActionName, NotFoundIdInfo.ControllerName, new { categorie = "Room" });
        }
    }
}
