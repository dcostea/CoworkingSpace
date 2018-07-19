using CoworkingSpace.Controllers;
using CoworkingSpace.Models;
using CoworkingSpace.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestCoworking
{
    [TestClass]
    public class ReservationControllerUT
    {
        private Mock<IReservationRepository> reservationRepoMock;

        [TestMethod]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            var reservations = new List<Reservation>
            {
                new Reservation { CustomerId = 1, MembershipId = 1, Details = "something" },
                new Reservation { CustomerId = 2, MembershipId = 2, Details = "something else" },
            };
            reservationRepoMock = new Mock<IReservationRepository>();
            reservationRepoMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(await Task.FromResult(reservations));
            var controller = new ReservationsController(reservationRepoMock.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>();
            var model = (IEnumerable<Reservation>)viewResult.Subject.Model;
            model.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task EditPost_ValidInput_ReturnsViewResult()
        {
            // Arrange
            var reservation = new Reservation { ReservationId = 2, CustomerId = 2, MembershipId = 2, Details = "something new" };
            reservationRepoMock = new Mock<IReservationRepository>();
            reservationRepoMock
                .Setup(repo => repo.UpdateAsync(reservation))
                .Returns(Task.FromResult(1));
            var controller = new ReservationsController(reservationRepoMock.Object);

            // Act
            var result = await controller.Edit(2, reservation);

            // Assert
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>();
            redirectToActionResult.Subject.ActionName.Should().Be("Index");
        }

        [TestMethod]
        public async Task EditPost_InconsistentInput_ReturnsNotFoundResult()
        {
            // Arrange
            var reservation = new Reservation { ReservationId = 2, CustomerId = 2, MembershipId = 2, Details = "something new" };
            reservationRepoMock = new Mock<IReservationRepository>();
            reservationRepoMock
                .Setup(repo => repo.UpdateAsync(reservation))
                .Returns(Task.FromResult(1));
            var controller = new ReservationsController(reservationRepoMock.Object);

            // Act
            var result = await controller.Edit(1, reservation);

            // Assert
            var redirectToActionResult = result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task EditPost_InvalidModelState_ReturnsNotFoundResult()
        {
            // Arrange
            var reservation = new Reservation { ReservationId = 2 };
            reservationRepoMock = new Mock<IReservationRepository>();
            reservationRepoMock
                .Setup(repo => repo.UpdateAsync(reservation))
                .Returns(Task.FromResult(1));
            var controller = new ReservationsController(reservationRepoMock.Object);
            controller.ModelState.AddModelError("Details", "Length too large");

            // Act
            var result = await controller.Edit(2, reservation);

            // Assert
            var viewResult = result.Should().BeOfType<ViewResult>();
            var model = (Reservation)viewResult.Subject.Model;
            model.Should().Be(reservation);
        }
    }
}
