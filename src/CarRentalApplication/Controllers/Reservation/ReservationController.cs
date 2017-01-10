using CarRentalApplication.Models.ViewModels.Reservation;
using CarRentalApplication.Repositories;
using CarRentalApplication.Services;
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
        private ViewModelSesssionService _sessionService;

        public ReservationController(VehicleRepository vehicleRepo, ViewModelSesssionService sessionService)
        {
            _vehicleRepo = vehicleRepo;
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var currModel = _sessionService.GetFromSession<ReservationLogisticsViewModel>(HttpContext, ReservationLogisticsViewModel.SessionKey)
                ?? null;
            return View(currModel);
        }

        [HttpPost]
        public IActionResult Index(ReservationLogisticsViewModel rlvm)
        {
            if (ModelState.IsValid)
            {
                _sessionService.SaveToSession(HttpContext, rlvm, ReservationLogisticsViewModel.SessionKey);
                return RedirectToAction("VehicleSetup");
            }
            return View(rlvm);
        }

        public IActionResult VehicleSetup()
        {
            var currModel = _sessionService.GetFromSession<ReservationVehicleViewModel>(HttpContext, ReservationVehicleViewModel.SessionKey)
                ?? new ReservationVehicleViewModel { AvailableVehicles = _vehicleRepo.GetAllAvailableVehicles() };
            return View(currModel);
        }

        [HttpPost]
        public IActionResult VehicleSetup(ReservationVehicleViewModel rvvm)
        {
            if (ModelState.IsValid)
            {
                rvvm.Vehicle = _vehicleRepo.GetVehicleById(rvvm.VehicleId);
                _sessionService.SaveToSession(HttpContext, rvvm, ReservationVehicleViewModel.SessionKey);
                return RedirectToAction("ContactSetup");
                //Save the View Model in the Session Object.
            }
            return View(rvvm);
        }

        public IActionResult ContactSetup()
        {
            var currModel = _sessionService.GetFromSession<ReservationContactViewModel>(HttpContext, ReservationContactViewModel.SessionKey)
                            ?? new ReservationContactViewModel();
            return View(currModel);
        }

        [HttpPost]
        public IActionResult ContactSetup(ReservationContactViewModel rcvm)
        {
            if (ModelState.IsValid)
            {
                _sessionService.SaveToSession(HttpContext, rcvm, ReservationContactViewModel.SessionKey);
                return RedirectToAction("Review");
            }
            return View(rcvm);
        }

        public IActionResult Review()
        {
            var logisticsSetup = _sessionService.GetFromSession<ReservationLogisticsViewModel>(HttpContext,ReservationLogisticsViewModel.SessionKey);
            var vehicleSetup = _sessionService.GetFromSession<ReservationVehicleViewModel>(HttpContext, ReservationVehicleViewModel.SessionKey);
            var contactSetup = _sessionService.GetFromSession<ReservationContactViewModel>(HttpContext, ReservationContactViewModel.SessionKey);
            var currModel = new ReservationViewModel
            {
                LogisticsSetup = logisticsSetup,
                VehicleSetup = vehicleSetup,
                ContactSetup = contactSetup
            };
            return View(currModel);
        }


    }
}
