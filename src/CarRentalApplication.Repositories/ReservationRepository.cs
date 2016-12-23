using CarRentalApplication.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Repositories
{
    public class ReservationRepository
    {
        private AppDbContext _context;
        private UserManager<AppUser> _userManager;

        public ReservationRepository(AppDbContext context, UserManager<AppUser> userMgr)
        {
            _context = context;
            _userManager = userMgr;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public Reservation FindReservationByConfirmationNumber(long confirmationNumber)
        {
            return _context.Reservations.Find(confirmationNumber);
        }

        public bool AddNewReservation(Reservation newReservation, string userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.Reservations.Add(newReservation);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool UpdateReservation(Reservation reservationToUpdate)
        {
            _context.Reservations.Attach(reservationToUpdate);
            _context.Entry(reservationToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public Reservation FindReservationByUser(long confirmationNumber, AppUser user)
        {
            return _context.Reservations.Where(x => x.ConfirmationNumber == confirmationNumber && string.Compare(x.AppUserId, user.Id) == 0).FirstOrDefault();
        }

        public bool DeleteReservation(Reservation reservationToDelete)
        {
            _context.Reservations.Remove(reservationToDelete);
            var isdeleted = _context.SaveChanges();
            return true;
        }

        public IEnumerable<Reservation> GetReservationsForUser(string userId)
        {
            return _context.Reservations.Where(x => x.AppUserId == userId).ToList();
        }
    }
}
