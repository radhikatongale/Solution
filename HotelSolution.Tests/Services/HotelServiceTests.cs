using HotelSolution.Models;
using HotelSolution.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace HotelSolution.Tests.Services
{
    [TestFixture]
    public class HotelServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            var json = JsonConvert.SerializeObject(GetSampleHotels());
            File.WriteAllText("hotels.json", json);
        }

        [Test]
        public void GetAllHotels_ReturnsHotels()
        {
            // Arrange
            var service = new HotelService();

            // Act
            var hotels = service.GetAllHotels();

            // Assert
            Assert.NotNull(hotels);
            Assert.IsNotEmpty(hotels);
        }

        [Test]
        public void GetHotelById_ValidId_ReturnsHotel()
        {
            // Arrange
            var service = new HotelService();

            // Act
            var hotel = service.GetHotelById(1);

            // Assert
            Assert.NotNull(hotel);
            Assert.AreEqual(1, hotel.Id);
        }

        [Test]
        public void GetHotelById_InvalidId_ReturnsNull()
        {
            // Arrange
            var service = new HotelService();

            // Act
            var hotel = service.GetHotelById(999);

            // Assert
            Assert.IsNull(hotel);
        }

        private IEnumerable<Hotel> GetSampleHotels()
        {
            return new List<Hotel>
            {
                new Hotel { Id = 1, Name = "Sample Hotel 1", Location = "Sample Location 1" },
                new Hotel { Id = 2, Name = "Sample Hotel 2", Location = "Sample Location 2" }
            };
        }
    }
}
