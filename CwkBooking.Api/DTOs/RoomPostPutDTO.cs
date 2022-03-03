namespace CwkBooking.Api.DTOs
{
    public class RoomPostPutDTO
    {
        public int RoomNumber { get; set; }
        public double Surface { get; set; }
        public bool NeedsRepair { get; set; }
    }
}
