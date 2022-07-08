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
using KotClassLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using KotClassLibrary.Helpers;

namespace SkyKotApp.Controllers
{
    [Authorize(Roles = Roles.Admin + ", " + Roles.Owner)]
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISkyKotRepository skyKotRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public RoomController(AppDbContext context,
            ISkyKotRepository skyKotRepository,
             IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.skyKotRepository = skyKotRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Room
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        // GET: Room/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return  RedirecToNotFound();
            }

            var room = await skyKotRepository.GetRoom(id);
            
            if (room == null)
            {
                return  RedirecToNotFound();
            }
            else if(!room.IsAvailable)
            {
                if (skyKotRepository.GetCurrentUserRole() != Roles.Admin)
                {
                    //check if is owner of the Room
                    if (!await skyKotRepository.IsOwnRoom(id))
                    {
                        return  RedirecToNotFound();
                    }
                }
            }

            return View(room);
        }

        // GET: Room/Create
        public async Task<IActionResult> Create()
        {
            RoomCreateViewModel model = new RoomCreateViewModel()
            {
                RoomSpecificationsList = await skyKotRepository.GetRoomSpecificationsToCreate(),
                RoomExpensesList = await skyKotRepository.GetRoomExpenseToCreate(),
                HousesSelectList = await skyKotRepository.GetHousesSelectList(),
                AvailableFrom = DateTime.Now
            };
            return View(model);
        }

        // POST: Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("RoomId,HouseId,RoomNumber,RoomType,MaxPeople,Period,AvailableFrom,IsAvailable")]
        public async Task<IActionResult> Create(RoomCreateViewModel model)
        {
            model.RoomSpecificationsList = await skyKotRepository.GetRoomSpecificationsToCreate();
            model.RoomExpensesList = await skyKotRepository.GetRoomExpenseToCreate();
            model.HousesSelectList = await skyKotRepository.GetHousesSelectList();
            if (ModelState.IsValid)
            {
                //check date
                if(model.AvailableFrom < DateTime.Now)
                {
                    ModelState.AddModelError("AvailableFrom", "Date must be in the future");
                    return View(model);
                }
                if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
                {
                    //check if is owner of the house
                    if (!await skyKotRepository.IsOwnHouseAsync(model.HouseId))
                    {
                        ModelState.AddModelError("", "Not your House, fuck you");
                        return View(model);
                    }
                }
                model.ImagesPaths = PhotoHelper.ProcessUploadedFile(hostingEnvironment, model.Photos, "Room");
                await skyKotRepository.AddRoom(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return  RedirecToNotFound();
            }

            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the Room
                if (!await skyKotRepository.IsOwnRoom(id))
                {
                    return  RedirecToNotFound();
                }
            }

            Room room = await skyKotRepository.GetRoom(id);
            if (room == null)
            {
                return  RedirecToNotFound();
            }

            RoomEditViewModel model = new RoomEditViewModel(room)
            {
                RoomSpecificationsList = await skyKotRepository.GetRoomSpecificationsToEdit(room.RoomSpecifications),
                RoomExpensesList = await skyKotRepository.GetRoomExpenseToCreate(),
                HousesSelectList = await skyKotRepository.GetHousesSelectList(),
            };

            return View(model);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("RoomId,HouseId,RoomNumber,RoomType,MaxPeople,Period,AvailableFrom,IsAvailable")] 
        public async Task<IActionResult> Edit(int id, RoomEditViewModel model)
        {
            if (id != model.RoomId)
            {
                return  RedirecToNotFound();
            }


            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the Room
                if (!await skyKotRepository.IsOwnRoom(id) || !await skyKotRepository.IsOwnHouseAsync(model.HouseId))
                {
                    return  RedirecToNotFound();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.RoomImages != null)
                    {
                        PhotoHelper.DeletePhotos(hostingEnvironment, model.RoomImages);
                    }
                    model.ImagesPaths = PhotoHelper.ProcessUploadedFile(hostingEnvironment, model.Photos, "Room");
                    await skyKotRepository.UpdateRoom(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(model.RoomId))
                    {
                        return  RedirecToNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            model.RoomSpecificationsList = await skyKotRepository.GetRoomSpecificationsToCreate();
            model.RoomExpensesList = await skyKotRepository.GetRoomExpenseToCreate();
            model.HousesSelectList = await skyKotRepository.GetHousesSelectList();
            return View(model);
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return  RedirecToNotFound();
            }


            if (skyKotRepository.GetCurrentUserRole() == Roles.Owner)
            {
                //check if is owner of the Room
                if (!await skyKotRepository.IsOwnRoom(id))
                {
                    return  RedirecToNotFound();
                }
            }

            var room = await _context.Rooms
                .Include(r => r.House)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return  RedirecToNotFound();
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
                    return  RedirecToNotFound();
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
        private RedirectToActionResult RedirecToNotFound()
        {
            return RedirectToAction(NotFoundIdInfo.ActionName, NotFoundIdInfo.ControllerName, new { categorie = "Room" });
        }
    }
}
