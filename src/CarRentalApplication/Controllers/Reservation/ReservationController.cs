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

        [HttpPost]
        public IActionResult VehicleSetup(ReservationLogisticsViewModel rlvm)
        {
            if (ModelState.IsValid)
            {

            }
            return View(rlvm);
        }

    }
}
