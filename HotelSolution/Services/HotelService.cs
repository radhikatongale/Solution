using HotelSolution.Interfaces;
using HotelSolution.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HotelSolution.Services
{
    public class HotelService : IHotelService
    {
        private readonly List<Hotel> _hotels;

        public HotelService()
        {
            var jsonData = File.ReadAllText("hotels.json");
            _hotels = JsonConvert.DeserializeObject<List<Hotel>>(jsonData);
        }

        public IEnumerable<Hotel> GetAllHotels()
        {
            return _hotels;
        }

        public Hotel GetHotelById(int id)
        {
            return _hotels.FirstOrDefault(h => h.Id == id);
        }
    }
}
