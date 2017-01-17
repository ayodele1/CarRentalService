using CarRentalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Repositories
{
    public class ReservationContactRepository
    {
        private AppDbContext _context;

        public ReservationContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public ReservationContact CreateNew(ReservationContact rc)
        {
            if (rc != null)
            {
                if (!IsExistingInDB(rc))
                {
                    _context.ReservationContacts.Add(rc);
                    _context.SaveChanges();
                }
                return _context.ReservationContacts.Where(x => x.Email == rc.Email).FirstOrDefault();
            }
            return null;
        }

        public bool IsExistingInDB(ReservationContact rc)
        {
            if (_context.ReservationContacts.Where(x => x.Email == rc.Email).FirstOrDefault() != null)
                return true;
            return false;
        }

        public ReservationContact FindByEmail(string email)
        {
            return _context.ReservationContacts.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
