using HotelSolution.Models;
using System.Collections.Generic;

namespace HotelSolution.Interfaces
{
    public interface IHotelService
    {
        IEnumerable<Hotel> GetAllHotels();
        Hotel GetHotelById(int id);
    }
}
