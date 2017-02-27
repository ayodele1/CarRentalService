using CarRentalApplication.Models;
using CarRentalApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Api.Controllers
{
    [Authorize]
    [Route("api/Reservations")]
    public class ReservationsController : Controller
    {
        private ReservationRepository _reservationRepo;
        private UserManager<AppUser> _userMgr;

        public ReservationsController(ReservationRepository reservationRepo,
            UserManager<AppUser> userMgr)
        {
            _reservationRepo = reservationRepo;
            _userMgr = userMgr;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId =  _userMgr.GetUserId(HttpContext.User);
                var user = await _userMgr.FindByIdAsync(userId);
                if (user != null)
                {
                    var reservations = _reservationRepo.GetReservationsForUser(user.Id);
                    if (reservations == null || reservations.ToList().Count == 0)
                        return NotFound($"User {user.UserName} has no reservations");

                    return Ok(reservations);
                }
            }
            return BadRequest();
        }


        [HttpGet("{confirmationNumber}")]
        public IActionResult Get(long confirmationNumber)
        {
            try
            {
                var reservation = _reservationRepo.GetReservationByConfirmationNumber(confirmationNumber);
                if (reservation == null)
                    return NotFound($"Reservation with {confirmationNumber} not found");

                return Ok(reservation);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
