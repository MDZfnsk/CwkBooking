using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace CwkBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController: Controller
    {
        private readonly DataSource _dataSource;
        public HotelsController(DataSource dataSource)
        {
            _dataSource = dataSource;
            
        }

        //Get all Hotels
        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var hotels = _dataSource.Hotels;
            return Ok(hotels);
        }


        //Get one Hotel by Id
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = _dataSource.Hotels;
            var hotel = hotels.FirstOrDefault(h => h.HotelId == id);

            if(hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        //Post a new Hotel
        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            var hotels = _dataSource.Hotels;
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById),new {id = hotel.HotelId }, hotel);
        }


        //Update an Existing Hotel
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel updated, int id)
        {
            var hotels = _dataSource.Hotels;
            var old = hotels.FirstOrDefault(h => h.HotelId == id);

            if(old == null)
                return NotFound("No Hotel with the corresponding Id found");

            hotels.Remove(old);
            hotels.Add(updated);
            return NoContent();
        }

        
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var hotels = _dataSource.Hotels;
            var toDelete = hotels.FirstOrDefault(h => h.HotelId == id);

            if (toDelete == null)
                return NotFound("No Hotel with the corresponding Id found");

            hotels.Remove(toDelete);           
            return NoContent();
        }


       

    }
}
