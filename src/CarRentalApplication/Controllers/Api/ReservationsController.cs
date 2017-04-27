using AutoMapper;
using CarRentalApplication.Models;
using CarRentalApplication.Models.ViewModels.Api;
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
        private ReservationContactRepository _reservationContactRepo;
        private ReservationRepository _reservationRepo;
        private UserManager<AppUser> _userMgr;

        public ReservationsController(ReservationRepository reservationRepo,
            UserManager<AppUser> userMgr,
            ReservationContactRepository rcr)
        {
            _reservationRepo = reservationRepo;
            _userMgr = userMgr;
            _reservationContactRepo = rcr;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool includevehicledetails = false)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userId = _userMgr.GetUserId(HttpContext.User);
                var user = await _userMgr.FindByIdAsync(userId);
                if (user != null)
                {
                    IEnumerable<Reservation> reservations = includevehicledetails ? _reservationRepo.GetReservationsForUser(user,true)
                        : _reservationRepo.GetReservationsForUser(user);
                    if (reservations == null || reservations.ToList().Count == 0)
                        return NotFound($"User {user.UserName} has no reservations");
                    return Ok(Mapper.Map<IEnumerable<ApiReservationViewModel>>(reservations));
                }
            }
            return BadRequest("User Identity could not be verified");
        }


        [HttpGet("{confirmationNumber}")]
        public IActionResult Get(long confirmationNumber)
        {
            try
            {
                var reservation = _reservationRepo.GetReservationByConfirmationNumber(confirmationNumber);
                if (reservation == null)
                    return NotFound($"Reservation with {confirmationNumber} not found");

                return Ok(Mapper.Map<ApiReservationViewModel>(reservation));
            }
            catch (Exception)
            {
                return BadRequest("Error occured getting reservation");
            }
        }

        [HttpPut("{confirmationNumber}")]
        public IActionResult Put(long confirmationNumber, [FromBody]ApiReservationViewModel updatedReservation)
        {
            var reservationToUpdate = Mapper.Map<Reservation>(updatedReservation);
            var oldReservation = _reservationRepo.GetReservationByConfirmationNumber(confirmationNumber);
            if (oldReservation == null) return NotFound($"Reservation with {confirmationNumber} could not be found");

            Mapper.Map<Reservation, Reservation>(reservationToUpdate,oldReservation);

            if (!_reservationRepo.UpdateReservation(oldReservation))
                return BadRequest($"{confirmationNumber} could not be updated");

            return Ok($"{confirmationNumber} has been updated");
        }

        [HttpDelete("{confirmationNumber}")]
        public IActionResult Delete(long confirmationNumber)
        {
            var reservationToCancel = _reservationRepo.GetReservationByConfirmationNumber(confirmationNumber);
            if (reservationToCancel == null)
                return NotFound($"Reservation with {confirmationNumber} not found");

            try
            {
                if (!_reservationRepo.DeleteReservation(reservationToCancel))
                    return BadRequest("Error occured while deleting");

                return Ok($"Reservation {confirmationNumber} has been Cancelled");
            }
            catch (Exception)
            {
                return BadRequest("Action could not be completed");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]ApiReservationViewModel newReservation)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userReservation = Mapper.Map<Reservation>(newReservation);
            var reservationOwner = _reservationContactRepo.CreateNew(userReservation.ReservationContact);
            var created = _reservationRepo.CreateNewReservation(userReservation, reservationOwner.Id);
            if (created != null)
            { 
                if (this.User.Identity.IsAuthenticated)
                {
                    var userId = _userMgr.GetUserId(HttpContext.User);
                    if(!_reservationRepo.AddReservationToUser(created, userId))
                    {
                        return BadRequest();
                    }
                }
                return Created("", Mapper.Map<ApiReservationViewModel>(created));
            }

            return BadRequest("error occured");
        }
    }
}
