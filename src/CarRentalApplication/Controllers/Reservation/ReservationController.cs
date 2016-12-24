using CarRentalApplication.Models.ViewModels.Reservation;
using CarRentalApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Controllers.Reservation
{
    public class ReservationController : Controller
    {
        private VehicleRepository _vehicleRepo;

        public ReservationController(VehicleRepository vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }
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

        public IActionResult VehicleSetup()
        {
            var viewModel = new ReservationVehicleViewModel { AvailableVehicles = _vehicleRepo.GetAllAvailableVehicles() };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult VehicleSetup(ReservationVehicleViewModel rvvm)
        {            
            if (ModelState.IsValid)
            {
                return RedirectToAction("ContactSetup");
                //Save the View Model in the Session Object.
            }
            return View(rvvm);
        }

        public IActionResult ContactSetup()
        {
            return View();
        }

    }
}
