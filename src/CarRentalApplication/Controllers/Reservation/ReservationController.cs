using CarRentalApplication.Models.ViewModels.Reservation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Controllers.Reservation
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ReservationLogisticsViewModel rlvm)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("VehicleSetup");
            }
            return View(rlvm);
        }

        public IActionResult VehicleSetup(ReservationLogisticsViewModel rlvm)
        {
            return View();
        }

    }
}
