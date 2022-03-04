using CwkBooking.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CwkBooking.Domain.Abstracts.Services
{
    public interface IReservationService
    {
        Task<Reservation> MakeReservation(Reservation reservation);
    }
}
