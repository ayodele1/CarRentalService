using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarRentalApplication.Models.ViewModels.Reservation;
using Microsoft.AspNetCore.Identity;
using CarRentalApplication.Models;
using CarRentalApplication.Repositories;

namespace CarRentalApplication.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager;
        private ReservationContactRepository _reservationContactRepo;
        private ReservationRepository _reservationRepo;

        public HomeController(UserManager<AppUser> userMgr,ReservationContactRepository rcr,ReservationRepository rp)
        {
            _userManager = userMgr;
            _reservationContactRepo = rcr;
            _reservationRepo = rp;
        }
        public IActionResult Index()
        {
            return View(new ReservationLogisticsViewModel());
        }

        public async Task<IActionResult> SearchReservation()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = (await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))).Email;
                return View(new ReservationSearchViewModel { Email = userEmail, ConfirmationNumber = null });
            }
            return View();
        }

        [HttpPost]
        public IActionResult SearchReservation(ReservationSearchViewModel rsvm)
        {
            if (ModelState.IsValid)
            {
                var reservationContact = _reservationContactRepo.FindByEmail(rsvm.Email);                
                if(reservationContact != null)
                {
                    var reservation = _reservationRepo.FindReservationByContact(reservationContact);
                    if (reservation == null)
                    {
                        ModelState.AddModelError(string.Empty, "Reservation Could not be Found");
                    }
                    return RedirectToAction("Update", "Reservation", reservation);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Reservation Could not be Found");
                }
            }
            return View(rsvm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
