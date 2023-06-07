using CwkBooking.Api.Services;
using CwkBooking.Dal;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CwkBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController: Controller
    {
        
        private readonly DataSourceService _dataSourceService;
        private readonly ILogger<HotelsController> _logger;
        private readonly HttpContext _http;

        private readonly DataContext _ctx;

        public HotelsController(
            DataSourceService dataSourceService,           
            ILogger<HotelsController> logger,
            IHttpContextAccessor httpContextAccessor,
            DataContext ctx)
        {
            _dataSourceService = dataSourceService;         
            _logger = logger;
            _http = httpContextAccessor.HttpContext;
            _ctx = ctx;
        }

        //Get all Hotels
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            return Ok(hotels);
        }


        //Get one Hotel by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
           var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            if(hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        //Post a new Hotel
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            _ctx.Hotels.Add(hotel);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
           
        }


        //Update an Existing Hotel
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel updated, int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            if(hotel == null)
                return NotFound("No Hotel with the corresponding Id found");

            hotel.Stars = updated.Stars;
            hotel.Description = updated.Description;
            hotel.Name = updated.Name;

            _ctx.Hotels.Update(hotel);
            await _ctx.SaveChangesAsync();
            
            return NoContent();
        }

        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            _ctx.Hotels.Remove(hotel);
            await _ctx.SaveChangesAsync();

            return NoContent();           
        }


       

    }
}
