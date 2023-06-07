using CwkBooking.Api.Services;
using CwkBooking.Api.Services.Abstractions;
using CwkBooking.Domain.Models;
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

        private readonly ISingletonOperation _singleton;
        private readonly ITransientOperation _transient;
        private readonly IScopedOperation _scoped;

        private readonly ILogger<HotelsController> _logger;
        public HotelsController(
            DataSourceService dataSourceService,
            ISingletonOperation singleton, 
            ITransientOperation transient, 
            IScopedOperation scoped,
            ILogger<HotelsController> logger)
        {
            _dataSourceService = dataSourceService;
            _singleton = singleton;
            _transient = transient;
            _scoped = scoped;
            _logger = logger;
        }

        //Get all Hotels
        [HttpGet]
        public IActionResult GetAllHotels()
        {
            _logger.LogInformation($"GUID of singleton : {_singleton.Guid}");
            _logger.LogInformation($"GUID of transient : {_transient.Guid}");
            _logger.LogInformation($"GUID of scoped : {_scoped.Guid}");

            var hotels = _dataSourceService.GetHotels();
            return Ok(hotels);
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
