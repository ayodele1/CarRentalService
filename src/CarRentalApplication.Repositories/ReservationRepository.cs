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
        private Random random = new Random();

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

        public Reservation CreateNewReservation(Reservation newReservation, Guid reservationContactId)
        {
            if (newReservation != null)
            {
                var reservationContact = _context.ReservationContacts.Find(reservationContactId);
                if (reservationContact != null)
                {
                    newReservation.ConfirmationNumber = random.Next(100000000, 999999999);
                    reservationContact.Reservations.Add(newReservation);
                    _context.SaveChanges();
                    return _context.Reservations.Where(x => x.ConfirmationNumber == newReservation.ConfirmationNumber).FirstOrDefault();
                }                
            }
            return null;
        }

        public bool AddReservationToUser(Reservation newReservation, string userId)
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
            _context.Entry(reservationToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public Reservation FindReservationByUser(long confirmationNumber, AppUser user)
        {
            return _context.Reservations.Where(x => x.ConfirmationNumber == confirmationNumber && string.Compare(x.AppUserId, user.Id) == 0).FirstOrDefault();
        }

        public Reservation FindReservationByContact(ReservationContact rc)
        {
            return _context.Reservations.Where(x => x.ReservationContact.Id == rc.Id).FirstOrDefault();
        }

        public Reservation GetReservation(long confirmationNumber, Guid reservationContactId)
        {
            return _context.Reservations.Where(x => x.ReservationContactId == reservationContactId && x.ConfirmationNumber == confirmationNumber).FirstOrDefault();
        }

        public bool DeleteReservation(Reservation reservationToDelete)
        {
            _context.Entry(reservationToDelete).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;           
            var isdeleted = _context.SaveChanges();
            return true;
        }

        public IEnumerable<Reservation> GetReservationsForUser(string userId)
        {
            return _context.Reservations.Where(x => x.AppUserId == userId).ToList();
        }
    }
}
