using CwkBooking.Dal;
using CwkBooking.Domain.Abstracts.Repositories;
using CwkBooking.Domain.Abstracts.Services;
using CwkBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkBooking.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelsRepository _hotelRepository;
        private readonly DataContext _ctx;

        public ReservationService(IHotelsRepository hotelRepo, DataContext ctx)
        {
            _hotelRepository = hotelRepo;
            _ctx = ctx;
        }

        public async Task<Reservation> MakeReservation(Reservation reservation)
        {
            // Step 1: Create a reservation instance 
            //var reservation = new Reservation
            //{
            //    HotelId = hotelId,
            //    RoomId = roomId,
            //    CheckInDate = checkIn,
            //    CheckOutDate = checkOut,
            //    Customer = customer
            //};

            // Step 2: Get the hotel, including all rooms
            var hotel = await _hotelRepository.GetHotelByIdAsync(reservation.HotelId);

            // Step 3: Find the specified room
            var room = hotel.Rooms.Where(r => r.RoomId == reservation.RoomId).FirstOrDefault();

            // Step 4: Make sure the room is available
            var roomBusyFrom = room.BusyFrom == null ? default(DateTime) : room.BusyFrom;
            var roomBusyTo = room.BusyTo == null ? default(DateTime) : room.BusyTo;
            var isBusy = reservation.CheckInDate >= roomBusyFrom && reservation.CheckInDate <= roomBusyTo;

            if (isBusy || room.NeedsRepair)
                return null;

            // Step 5: Set busyFrom and busyTo on the room
            room.BusyFrom = reservation.CheckInDate;
            room.BusyTo = reservation.CheckOutDate;

            // Step 6: Persist all changes to database
            _ctx.Rooms.Update(room);
            _ctx.Reservations.Add(reservation);
            await _ctx.SaveChangesAsync();

            return reservation;
        }
    }
}
