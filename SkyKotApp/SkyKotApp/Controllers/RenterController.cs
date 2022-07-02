using KotClassLibrary.Helpers;
using KotClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyKotApp.Data;
using SkyKotApp.Data.Default;
using SkyKotApp.Services.General;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Controllers
{
    [Authorize(Roles = Roles.Admin + ", " + Roles.Renter)]
    public class RenterController : Controller
    {
        private readonly ISkyKotRepository skyKotRepository;
        private readonly AppDbContext dbContext;

        public RenterController(ISkyKotRepository skyKotRepository, AppDbContext dbContext)
        {
            this.skyKotRepository = skyKotRepository;
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await skyKotRepository.GetRenters());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return RedirecToNotFound();
            }

            var renterRoom = await skyKotRepository.GetRenter(id);

            if (renterRoom == null)
            {
                return RedirecToNotFound();
            }
            return View(renterRoom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay(int renterContractId)
        {
            if(renterContractId == 0)
            {
                return RedirecToNotFound();
            }
            RenterContract contract = await skyKotRepository.GetRenterContract(renterContractId);
            if (contract == null)
            {
                return RedirecToNotFound();
            }
            
            float toPay = contract.RenterRoom.AmountToPay;

            contract.IsPayed = true;

            await skyKotRepository.UpdateRenterContract(contract);

            return RedirectToAction("Details", new {id = contract.RenterRoomId});
        }
        private RedirectToActionResult RedirecToNotFound()
        {
            return RedirectToAction(NotFoundIdInfo.ActionName, NotFoundIdInfo.ControllerName, new { categorie = "Renter Room" });
        }
    }
}
