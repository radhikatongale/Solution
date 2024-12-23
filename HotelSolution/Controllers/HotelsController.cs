using HotelSolution.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HotelSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            try
            {
                var hotels = _hotelService.GetAllHotels();
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the hotels.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetHotelById(string id)
        {
            try
            {
                if (!int.TryParse(id, out var hotelId))
                {
                    return BadRequest(new { Message = "Invalid hotel ID format. Please provide a numeric value." });
                }

                var hotel = _hotelService.GetHotelById(hotelId);
                if (hotel == null)
                {
                    return NotFound(new { Message = "Hotel not found" });
                }
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the hotel details.", Details = ex.Message });
            }
        }
    }
}
