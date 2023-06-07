using CwkBooking.Domain.Models;
using System.Collections.Generic;

namespace CwkBooking.Api.Services
{
    public class DataSourceService
    {
        private readonly DataSource _dataSource;
        public DataSourceService(DataSource dataSource)
        {

            _dataSource = dataSource;

        }

        public List<Hotel> GetHotels()
        {
            return _dataSource.Hotels;

        }
    }
}
