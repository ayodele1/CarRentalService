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
                _context.Users.Add(user);
            }
            if (!_context.Vehicles.Any())
            {
                var vehicles = new List<Vehicle>
                {
                    new Vehicle { MakeYear = 2015, Name = "Kia Rio", PassengerCapacity = 4, PricePerDay = 22, isAvailable = 1, ImageName = "kiario.png", ModelType = VehicleType.Car, WheelDrive = WheelDrive.FrontWheel },
                    new Vehicle { MakeYear = 2016, Name = "Nissan Versa Note", PassengerCapacity = 5, PricePerDay = 29.60, isAvailable = 1, ImageName = "nissanversa.png", ModelType = VehicleType.Car, WheelDrive = WheelDrive.FrontWheel },
                    new Vehicle { MakeYear = 2014, Name = "Hyundai Elantra", PassengerCapacity = 5, PricePerDay = 24.58, isAvailable = 1, ImageName = "hyundaielantra.png", ModelType = VehicleType.Car, WheelDrive = WheelDrive.FrontWheel },
                    new Vehicle { MakeYear = 2015, Name = "Buik LaCrosse", PassengerCapacity = 6, PricePerDay = 47.86, isAvailable = 1, ImageName = "buicklacrosse.png", ModelType = VehicleType.Car, WheelDrive = WheelDrive.FourWheel },
                    new Vehicle { MakeYear = 2016, Name = "Nissan Altima", PassengerCapacity = 6, PricePerDay = 33.45, isAvailable = 1, ImageName = "nissanaltima.png", ModelType = VehicleType.Car, WheelDrive = WheelDrive.RearWheel },
                    new Vehicle { MakeYear = 2013, Name = "Dodge Ram 1500 Quad", PassengerCapacity = 4, PricePerDay = 81.21, isAvailable = 1, ImageName = "dodgeram.png", ModelType = VehicleType.Truck, WheelDrive = WheelDrive.FourWheel },
                    new Vehicle { MakeYear = 2013, Name = "Cadillac XTS", PassengerCapacity = 4, PricePerDay = 97.79, isAvailable = 1, ImageName = "cadillacxts.png", ModelType = VehicleType.Luxury, WheelDrive = WheelDrive.RearWheel },
                    new Vehicle { MakeYear = 2016, Name = "Chrysler 200", PassengerCapacity = 4, PricePerDay = 31.99, isAvailable = 1, ImageName = "chrysler200.png", ModelType = VehicleType.Car, WheelDrive = WheelDrive.RearWheel },
                    new Vehicle { MakeYear = 2015, Name = "Toyota Rav4", PassengerCapacity = 5, PricePerDay = 69.89, isAvailable = 1, ImageName = "rav4.png", ModelType = VehicleType.Suv, WheelDrive = WheelDrive.AllWheel },
                    new Vehicle { MakeYear = 2014, Name = "GMC Terrain", PassengerCapacity = 6, PricePerDay = 76.43, isAvailable = 1, ImageName = "gmcterrain.png", ModelType = VehicleType.Suv, WheelDrive = WheelDrive.FourWheel },
                    new Vehicle { MakeYear = 2013, Name = "Nissan Frontier", PassengerCapacity = 4, PricePerDay = 90.26, isAvailable = 1, ImageName = "frontier.png", ModelType = VehicleType.Truck, WheelDrive = WheelDrive.FourWheel },
                    new Vehicle { MakeYear = 2014, Name = "Chevy Tahoe", PassengerCapacity = 7, PricePerDay = 95.35, isAvailable = 1, ImageName = "tahoe.png", ModelType = VehicleType.Suv, WheelDrive = WheelDrive.AllWheel },
                    new Vehicle { MakeYear = 2016, Name = "Tesla Model 3", PassengerCapacity = 4, PricePerDay = 127.81, isAvailable = 1, ImageName = "teslaModel3.jpe", ModelType = VehicleType.Luxury, WheelDrive = WheelDrive.AllWheel },
                    new Vehicle { MakeYear = 2013, Name = "Tesla Model S", PassengerCapacity = 4, PricePerDay = 135.97, isAvailable = 1, ImageName = "teslaS.jpe", ModelType = VehicleType.Luxury, WheelDrive = WheelDrive.FourWheel },
                    new Vehicle { MakeYear = 2015, Name = "Tesla Model X", PassengerCapacity = 5, PricePerDay = 153.66, isAvailable = 1, ImageName = "teslaModelX.jpe", ModelType = VehicleType.Luxury, WheelDrive = WheelDrive.AllWheel },
                };
                _context.Vehicles.AddRange(vehicles);
            }
            await _context.SaveChangesAsync();

        }
    }
}
