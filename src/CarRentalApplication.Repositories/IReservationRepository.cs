using System;
using System.Collections.Generic;
using CarRentalApplication.Models;

namespace CarRentalApplication.Repositories
{
    public interface IReservationRepository
    {
        bool AddReservationToUser(Reservation newReservation, string userId);
        Reservation CreateNewReservation(Reservation newReservation, Guid reservationContactId);
        bool DeleteReservation(long confirmationNumber);
        Reservation FindReservationByContact(ReservationContact rc);
        Reservation FindReservationByUser(long confirmationNumber, AppUser user);
        IEnumerable<Reservation> GetAll();
        Reservation GetReservation(long confirmationNumber, Guid reservationContactId);
        Reservation GetReservationByConfirmationNumber(long confirmationNumber, bool includeVehicleDetails = false, bool includeContactDetails = false, bool includeUserDetails = false);
        IEnumerable<Reservation> GetReservationsForUser(AppUser appUser, bool includeVehicleDetails = false, bool includeContactDetails = false);
        bool UpdateReservation(Reservation reservationToUpdate);
    }
}