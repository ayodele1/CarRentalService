using CarRentalApplication.Api.Controllers;
using CarRentalApplication.Models;
using CarRentalApplication.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CarRentalApplication.UnitTests.Controllers.Api
{
    [TestClass]
    public class ReservationsControllerTests
    {
        private ReservationsController _reservationApiController;
        private Mock<FakeUserManager> _mockUserManager;
        private Mock<IReservationRepository> _mockReservationRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockReservationRepository = new Mock<IReservationRepository>();

            _mockUserManager = new Mock<FakeUserManager>();          

            var mockReservationContactRepo = new Mock<IReservationContactRepository>();

            _reservationApiController = new ReservationsController(_mockReservationRepository.Object, _mockUserManager.Object, mockReservationContactRepo.Object);

            _reservationApiController.MockCurrentUser("1", "ayodele");
        }

        [TestMethod]
        public async Task Get_UserHasNoReservation_ShouldReturnNotFound()
        {
            var dummyUser = new AppUser() { UserName = "ayodele", Id = "1", FirstName = "ayodele", LastName = "awoleye" };
            _mockUserManager.Setup(x => x.GetUserId(_reservationApiController.User))
                .Returns("1");
            _mockUserManager.Setup(x => x.FindByIdAsync("1"))
                .Returns(Task.FromResult(dummyUser));

            var result = await _reservationApiController.Get();

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public async Task Get_UserCannotBeDetermined_ShouldReturnBadRequest()
        {
            var result = await _reservationApiController.Get();

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [TestMethod]
        public void Get_NoReservationWithGivenConfirmationNumberExists_ShouldReturnNotFound()
        {
            var result = _reservationApiController.Get(long.MaxValue);

            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [TestMethod]
        public void Get_ReservationWithGivenConfirmationNumberExists_ShouldReturnSucess()
        {
            //Arrange
            var dummyReservation = new Reservation
            {
                PickupLocation = "London",
                PickupDate = DateTime.Now,                    
                ReturnLocation = "London",
                UserLocation = "Brisbane",
                ReturnDate = DateTime.Now.AddDays(2),
            };
            _mockReservationRepository.Setup(x => x.GetReservationByConfirmationNumber(long.MaxValue, false, false, false))
                .Returns(dummyReservation);

            //Act
            var result = _reservationApiController.Get(long.MaxValue);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
