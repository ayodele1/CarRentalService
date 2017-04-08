
using AutoMapper;
using CarRentalApplication.Models;
using CarRentalApplication.Models.ViewModels.Dashboard;
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

namespace CarRentalApplication.Controllers.Dashboard
{
    [Authorize]
    public class DashboardController : Controller
    {
        private ReservationRepository _reservationRepository;
        private ViewModelSesssionService _sessionService;
        private UserManager<AppUser> _userManager;

        public DashboardController(ReservationRepository rr, UserManager<AppUser> userMgr, ViewModelSesssionService vmss)
        {
            _reservationRepository = rr;
            _userManager = userMgr;
            _sessionService = vmss;
        }
        public IActionResult Index()
        {
            var reservationViewModelList = new List<ReservationViewModel>();
            var signedInUserId = _userManager.GetUserId(HttpContext.User);
            var userReservations = _reservationRepository.GetReservationsForUser(signedInUserId,true,true);
            foreach (var reservation in userReservations)
            {
                var reservationViewModel = Mapper.Map<ReservationViewModel>(reservation);
                reservationViewModelList.Add(reservationViewModel);
            }
            var dashIndexViewModel = new ReservationsListViewModel { UserReservations = reservationViewModelList }; 
            return View(dashIndexViewModel);
        }

        [HttpPost]
        public IActionResult Index(ReservationsListViewModel rlvm)
        {
            var reservationToUpdate = _reservationRepository.GetReservationByConfirmationNumber(rlvm.ReservationId,true,true);
            var currModel = Mapper.Map<ReservationViewModel>(reservationToUpdate);
            _sessionService.SaveToSession(HttpContext, currModel, ReservationViewModel.SessionKey);
            return RedirectToAction("Update", "Reservation");
        }

        public IActionResult ReservationList()
        {
            return View();
        }
    }
}
