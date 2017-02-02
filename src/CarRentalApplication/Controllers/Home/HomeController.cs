﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarRentalApplication.Models.ViewModels.Reservation;
using Microsoft.AspNetCore.Identity;
using CarRentalApplication.Models;
using CarRentalApplication.Repositories;
using Microsoft.AspNetCore.Routing;
using CarRentalApplication.Services;
using AutoMapper;
using CarRentalApplication.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarRentalApplication.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager;
        private ReservationContactRepository _reservationContactRepo;
        private ReservationRepository _reservationRepo;
        private ViewModelSesssionService _sessionService;
        private VehicleRepository _vehicleRepo;

        public HomeController(UserManager<AppUser> userMgr, ReservationContactRepository rcr, ReservationRepository rp, ViewModelSesssionService vmss, VehicleRepository vrepo)
        {
            _userManager = userMgr;
            _reservationContactRepo = rcr;
            _reservationRepo = rp;
            _sessionService = vmss;
            _vehicleRepo = vrepo;
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
                if (reservationContact != null)
                {
                    var reservation = _reservationRepo.GetReservation((long)rsvm.ConfirmationNumber, reservationContact.Id);
                    if (reservation != null)
                    {
                        reservation.ReservationContact = reservationContact;
                        var reservationViewModel = Mapper.Map<ReservationViewModel>(reservation);
                        reservationViewModel.VehicleSetup.Vehicle = _vehicleRepo.GetVehicleById(reservationViewModel.VehicleSetup.VehicleId);
                        reservationViewModel.ContactSetup = Mapper.Map<ReservationContactViewModel>(reservationContact);
                        _sessionService.SaveToSession(HttpContext, reservationViewModel, ReservationViewModel.SessionKey);
                        return RedirectToAction("Update", "Reservation", new RouteValueDictionary(reservation));
                    }
                    ModelState.AddModelError(string.Empty, "Reservation Could not be Found");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Reservation Could not be Found");
                }
            }
            return View(rsvm);

        }

        public IActionResult CarInventory(string filter)
        {
            var vehicleProperty = new VehicleProperty { FilterName = "ModelType" };
            string currentPropertyFilter = "ModelType";
            var filteredVehicles = _vehicleRepo.GetVehiclesByFilter(vehicleProperty, filter);
            var civm = new CarInventoryViewModel { Vehicles = filteredVehicles, VehicleProperties = _vehicleRepo.GetVehicleFilterProperties(), SelectedFilter = filter, VehicleFilters = GetVehicleFilters("ModelType"), PropertyFilter = currentPropertyFilter };
            return View(civm);
        }

        [HttpPost]
        public IActionResult CarInventory(CarInventoryViewModel civm)
        {
            //If the User Selects a Different Vehicle Property
            civm.Vehicles = (string.Equals(civm.selectedVehiclePropery.FilterName, civm.PropertyFilter)) ?
                civm.Vehicles = _vehicleRepo.GetVehiclesByFilter(civm.selectedVehiclePropery, civm.SelectedFilter) :
                civm.Vehicles = _vehicleRepo.GetVehiclesByProperty(civm.selectedVehiclePropery);
            civm.PropertyFilter = civm.selectedVehiclePropery.FilterName;          
            civm.VehicleFilters = GetVehicleFilters(civm.selectedVehiclePropery.FilterName);
            civm.VehicleProperties = _vehicleRepo.GetVehicleFilterProperties();
            return View(civm);
        }

        private SelectList GetVehicleFilters(string filterProperty)
        {
            switch (filterProperty)
            {
                case "ModelType":
                    return _vehicleRepo.GetFiltersByModelTypes();
                case "MakeYear":
                    return _vehicleRepo.GetFilterByMakeYear();
                case "PassengerCapacity":
                    return _vehicleRepo.GetFilterByPassengerCapacity();
                case "WheelDrive":
                    return _vehicleRepo.GetFilterByWheelDrive();
                default:
                    return _vehicleRepo.GetFiltersByModelTypes();
            }
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
