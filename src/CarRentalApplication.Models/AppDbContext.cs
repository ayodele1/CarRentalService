using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRentalApplication.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private IConfigurationRoot _config;

        public AppDbContext(IConfigurationRoot config, DbContextOptions options)
            :base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder )
        {
            base.OnConfiguring(builder);
            var connectionString = _config.GetConnectionString("ConnectionString");
            builder.UseSqlServer(connectionString);
        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>().ToTable("Users", "dbo");
            builder.Entity<IdentityRole>().ToTable("Roles", "dbo");
        }

        /// <summary>
        /// Generate the Date Modified and Date Created values
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            foreach (var history in this.ChangeTracker.Entries()
                .Where(e => e.Entity is IModificationHistory && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .Select(e => e.Entity as IModificationHistory))
            {
                history.DateModified = DateTime.Now;
                if (history.DateCreated == DateTime.MinValue)
                    history.DateCreated = DateTime.Now;

            }
            return base.SaveChanges();
        }
    }
}
