using HotelSolution.Controllers;
using HotelSolution.Interfaces;
using HotelSolution.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace HotelSolution.Tests.Controllers
{
    [TestFixture]
    public class HotelsControllerTests
    {
        private Mock<IHotelService> _mockService;
        private HotelsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IHotelService>();
            _controller = new HotelsController(_mockService.Object);
        }

        [Test]
        public void GetAllHotels_ReturnsOkResult_WithListOfHotels()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllHotels()).Returns(GetSampleHotels());

            // Act
            var result = _controller.GetAllHotels();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsInstanceOf<IEnumerable<Hotel>>(okResult.Value);
        }

        [Test]
        public void GetHotelById_ValidId_ReturnsOkResult_WithHotel()
        {
            // Arrange
            var sampleHotel = GetSampleHotel(1);
            _mockService.Setup(service => service.GetHotelById(1)).Returns(sampleHotel);

            // Act
            var result = _controller.GetHotelById("1");

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(sampleHotel, okResult.Value);
        }

        [Test]
        public void GetHotelById_InvalidIdFormat_ReturnsBadRequest()
        {
            // Act
            var result = _controller.GetHotelById("invalid");

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void GetHotelById_NonexistentId_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetHotelById(999)).Returns((Hotel)null);

            // Act
            var result = _controller.GetHotelById("-1");

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        private IEnumerable<Hotel> GetSampleHotels()
        {
            return new List<Hotel>
            {
                new Hotel { Id = 1, Name = "Sample Hotel 1", Location = "Sample Location 1" },
                new Hotel { Id = 2, Name = "Sample Hotel 2", Location = "Sample Location 2" }
            };
        }

        private Hotel GetSampleHotel(int id)
        {
            return new Hotel { Id = id, Name = $"Sample Hotel {id}", Location = $"Sample Location {id}" };
        }
    }
}
