using AutoMapper;
using CarRentalApplication.Models;
using CarRentalApplication.Models.ViewModels.Reservation;
using CarRentalApplication.Repositories;
using CarRentalApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Controllers.VehicleReservation
{
    public class ReservationController : Controller
    {
        private VehicleRepository _vehicleRepo;
        private ViewModelSesssionService _sessionService;
        private ReservationRepository _reservationRepo;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private ReservationContactRepository _reservationContactRepo;

        public ReservationController(VehicleRepository vehicleRepo, ViewModelSesssionService sessionService, ReservationRepository resRepo, UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr, ReservationContactRepository rcontactrepo)
        {
            _vehicleRepo = vehicleRepo;
            _sessionService = sessionService;
            _reservationRepo = resRepo;
            _userManager = userMgr;
            _signInManager = signInMgr;
            _reservationContactRepo = rcontactrepo;
        }
        public IActionResult Index()
        {
            var currLogisticsSetup = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey).LogisticsSetup
                ?? null;
            //For testing Only. Remove after
            var test = new ReservationLogisticsViewModel
            {
                PickupDate = DateTime.Now,
                ReturnDate = DateTime.Now,
                PickupLocation = "987 Johnson Str, Pawtucket RI",
                ReturnLocation = "987 Johnson Str, Pawtucket RI",
                UserLocation = "02860 Pawtucket RI"
            };
            return View(test);
        }

        [HttpPost]
        public IActionResult Index(ReservationLogisticsViewModel rlvm)
        {
            if (ModelState.IsValid)
            {
                var currModel = new ReservationViewModel { LogisticsSetup = rlvm };
                _sessionService.SaveToSession(HttpContext, currModel, ReservationViewModel.SessionKey);
                return RedirectToAction("VehicleSetup");
            }
            return View(rlvm);
        }

        public IActionResult VehicleSetup()
        {
            var currVehicleSetup = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey).VehicleSetup
                ?? new ReservationVehicleViewModel { AvailableVehicles = _vehicleRepo.GetAllAvailableVehicles() };
            return View(currVehicleSetup);
        }

        [HttpPost]
        public IActionResult VehicleSetup(ReservationVehicleViewModel rvvm)
        {
            if (ModelState.IsValid)
            {
                rvvm.Vehicle = _vehicleRepo.GetVehicleById(rvvm.VehicleId);
                var currModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
                currModel.VehicleSetup = rvvm;
                _sessionService.SaveToSession(HttpContext, currModel, ReservationViewModel.SessionKey);
                return RedirectToAction("ContactSetup");
                //Save the View Model in the Session Object.
            }
            return View(rvvm);
        }

        public IActionResult ContactSetup()
        {
            var currContactSetup = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey).ContactSetup
                            ?? new ReservationContactViewModel();
            return View(currContactSetup);
        }

        [HttpPost]
        public IActionResult ContactSetup(ReservationContactViewModel rcvm)
        {
            if (ModelState.IsValid)
            {
                var currModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
                if (rcvm.Password != null)
                {
                    var newUser = CreateNewUser(rcvm);
                    if (newUser != null)
                    {
                        currModel.ApplicationUser = newUser.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "This Account already Exists");
                        return View(rcvm);
                    }
                }
                currModel.ReservationContact = Mapper.Map<ReservationContact>(rcvm);
                currModel.ContactSetup = rcvm;
                _sessionService.SaveToSession(HttpContext, currModel, ReservationViewModel.SessionKey);
                return RedirectToAction("Review");
            }
            return View(rcvm);
        }

        public IActionResult Review()
        {
            var currModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
            return View(currModel);
        }

        public IActionResult Confirmation()
        {
            var currModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
            if (currModel.ConfirmationNumber == default(long))
            {
                var reservation = Mapper.Map<Reservation>(currModel);
                var reservationOwner = _reservationContactRepo.CreateNew(currModel.ReservationContact);

                reservation = _reservationRepo.CreateNewReservation(reservation, reservationOwner.Id);
                currModel.ConfirmationNumber = reservation.ConfirmationNumber;
                if (User.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    _reservationRepo.AddReservationToUser(reservation, userId);
                }
                _sessionService.SaveToSession(HttpContext, currModel, ReservationViewModel.SessionKey);

            }
            return View(currModel);
        }

        [Authorize]
        public IActionResult Update()
        {
            var currModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
            return View(currModel);
        }

        public IActionResult VehicleUpdate()
        {
            var currVehicleSetup = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey).VehicleSetup;
            currVehicleSetup.AvailableVehicles = _vehicleRepo.GetAllAvailableVehicles();
            return View(currVehicleSetup);
        }

        [HttpPost]
        public IActionResult VehicleUpdate(ReservationVehicleViewModel rvvm)
        {
            rvvm.Vehicle = _vehicleRepo.GetVehicleById(rvvm.VehicleId);
            var currReservationViewModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
            currReservationViewModel.VehicleSetup = rvvm;
            _sessionService.SaveToSession(HttpContext, currReservationViewModel, ReservationViewModel.SessionKey);
            if (currReservationViewModel.ConfirmationNumber == default(long))
                return RedirectToAction("Review");
            return RedirectToAction("Update");
        }

        public IActionResult LogisticsUpdate()
        {
            var currLogisticsSetup = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey).LogisticsSetup;
            return View(currLogisticsSetup);
        }
        [HttpPost]
        public IActionResult LogisticsUpdate(ReservationLogisticsViewModel rlvm)
        {
            if (ModelState.IsValid)
            {
                var currReservationViewModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
                currReservationViewModel.LogisticsSetup = rlvm;
                _sessionService.SaveToSession(HttpContext, currReservationViewModel, ReservationViewModel.SessionKey);
                return RedirectToAction("Update ");
            }
            return View(rlvm);
        }

        public IActionResult ContactUpdate()
        {
            var currContactSetup = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey).ContactSetup;
            return View(currContactSetup);
        }

        [HttpPost]
        public IActionResult ContactUpdate(ReservationContactViewModel rcvm)
        {
            if (ModelState.IsValid)
            {
                var currReservationViewModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
                if (rcvm.Password != null)
                {
                    var newUser = CreateNewUser(rcvm);
                    if (newUser != null)
                    {
                        currReservationViewModel.ApplicationUser = newUser.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "This Account already Exists");
                        return View(rcvm);
                    }
                }
                currReservationViewModel.ReservationContact = Mapper.Map<ReservationContact>(rcvm);
                currReservationViewModel.ContactSetup = rcvm;
                _sessionService.SaveToSession(HttpContext, currReservationViewModel, ReservationViewModel.SessionKey);
                return RedirectToAction("Update");
            }
            return View(rcvm);
        }

        [Authorize]
        public IActionResult UpdateConfirmation()
        {
            var currReservationViewModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
            var reservationToUpdate = Mapper.Map<Reservation>(currReservationViewModel);
            _reservationRepo.UpdateReservation(reservationToUpdate);
            return View(currReservationViewModel);
        }

        [Authorize]
        public IActionResult CancelReservation()
        {
            var currReservationViewModel = _sessionService.GetFromSession<ReservationViewModel>(HttpContext, ReservationViewModel.SessionKey);
            var reservationToCancel = Mapper.Map<Reservation>(currReservationViewModel);
            _reservationRepo.DeleteReservation(reservationToCancel);
            return View(currReservationViewModel);
        }

        #region PrivateMethods

        private async Task<AppUser> CreateNewUser(ReservationContactViewModel rcvm)
        {
            if (await _userManager.FindByEmailAsync(rcvm.Email) == null)
            {
                var newUser = Mapper.Map<AppUser>(rcvm);
                try
                {
                    var userCreation = await _userManager.CreateAsync(newUser, rcvm.Password);
                    if (userCreation.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "customer");
                        await _signInManager.SignInAsync(newUser, false);
                        return newUser;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
        #endregion
    }
}
