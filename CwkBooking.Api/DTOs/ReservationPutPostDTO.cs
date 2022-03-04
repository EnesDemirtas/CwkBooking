using System;

namespace CwkBooking.Api.DTOs
{
    public class ReservationPutPostDTO
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Customer { get; set; }
    }
}
