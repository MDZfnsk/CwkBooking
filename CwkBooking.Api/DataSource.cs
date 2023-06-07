using CwkBooking.Domain.Models;
using System.Collections.Generic;

namespace CwkBooking.Api
{
    public class DataSource
    {
        public DataSource()
        {
            Hotels = GetHotels();
            
        }

        public List<Hotel> Hotels { get; set; }

        private List<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    HotelId = 1,
                    Name = "Hilton",
                    Stars = 5,
                    Country = "Sri Lanka",
                    City = "Colombo",
                    Description = "Nice hotel"

                },
                new Hotel
                {
                    HotelId = 2,
                    Name = "Galadari",
                    Stars = 5,
                    Country = "Sri Lanka",
                    City = "Colombo",
                    Description = "Super Cool hotel"

                }
            };
        }
        }
}
