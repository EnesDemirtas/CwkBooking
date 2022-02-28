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
                    Name = "HolidayInn",
                    Stars = 5,
                    Country = "Turkey",
                    City = "Ankara",
                    Description = "A description for HolidayInn"
                },

                new Hotel
                {
                    HotelId = 2,
                    Name = "The Westin",
                    Stars = 4,
                    Country = "USA",
                    City = "Seattle",
                    Description = "A description for The Westin"
                }
            };
        }
    }
}
