using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Reservations = new HashSet<Reservation>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
