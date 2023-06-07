using CwkBooking.Api.Services;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace CwkBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController: Controller
    {
        
        private readonly DataSourceService _dataSourceService;
        private readonly ILogger<HotelsController> _logger;

        private HttpContext _http;
        public HotelsController(
            DataSourceService dataSourceService,           
            ILogger<HotelsController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _dataSourceService = dataSourceService;         
            _logger = logger;
            _http = httpContextAccessor.HttpContext;
        }

        //Get all Hotels
        [HttpGet]
        public IActionResult GetAllHotels()
        {
            HttpContext.Request.Headers.TryGetValue("my-middleware-header", out var headerDate);
            return Ok(headerDate);

            //var hotels = _dataSourceService.GetHotels();
            //return Ok(hotels);
        }


        //Get one Hotel by Id
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = _dataSourceService.GetHotels();
            var hotel = hotels.FirstOrDefault(h => h.HotelId == id);

            if(hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        //Post a new Hotel
        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            var hotels = _dataSourceService.GetHotels();
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById),new {id = hotel.HotelId }, hotel);
        }


        //Update an Existing Hotel
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel updated, int id)
        {
            var hotels = _dataSourceService.GetHotels();
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
            var hotels = _dataSourceService.GetHotels();
            var toDelete = hotels.FirstOrDefault(h => h.HotelId == id);

            if (toDelete == null)
                return NotFound("No Hotel with the corresponding Id found");

            hotels.Remove(toDelete);           
            return NoContent();
        }


       

    }
}
