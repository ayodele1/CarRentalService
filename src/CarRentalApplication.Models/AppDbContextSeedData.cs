using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models
{
    public class AppDbContextSeedData
    {
        private AppDbContext _context;

        public AppDbContextSeedData(AppDbContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Users.Any())
            {
                var user = new AppUser
                {
                    Email = "awoleyeayodele1@gmail.com",
                    FirstName = "Ayodele",
                    LastName = "Awoleye",
                    UserName = "awoleyeayodele1@gmail.com",
                    PhoneNumber = "4012888776"
                };
            }
        }
    }
}
