using CarRentalApplication.Models;
using CarRentalApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.UnitTests.Repositories
{
    [TestClass]
    public class ReservationRepositoryTests
    {
        private IConfigurationRoot _config;
        private Mock<DbSet<Reservation>> _mockReservationDbSet;
        private ReservationRepository _reservationRepository;


        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test Reservation DB")
                .Options;

            var mockDbContext = new AppDbContext(_config, options);
            var mockUserManager = new Mock<FakeUserManager>();
            _mockReservationDbSet = new Mock<DbSet<Reservation>>();
            var reservation = new Reservation
            {
                PickupLocation = "London",
                PickupDate = DateTime.Now,
                VehicleId = Guid.NewGuid()
                    ,
                ReturnLocation = "London",
                TotalCost = 200,
                UserLocation = "Brisbane"
                    ,
                ReturnDate = DateTime.Now.AddDays(2),
                TotalVehicleCost = 100,
                ReservationContactId = Guid.NewGuid()
                    ,
                AppUserId = "abcde"
            };
            _mockReservationDbSet.SetSource(new[] { reservation });
            mockDbContext.Reservations = _mockReservationDbSet.Object;
            _reservationRepository = new ReservationRepository(mockDbContext, mockUserManager.Object);
        }

        [TestMethod]
        public void Should_Return_Null_When_Invalid_Confirmation_Number_Is_Used()
        {
            var valueReturned = _reservationRepository.GetReservationByConfirmationNumber(1);

            Assert.IsNull(valueReturned);
        }
    }
}
