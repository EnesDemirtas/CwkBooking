using AutoMapper;
using CwkBooking.Api.DTOs;
using CwkBooking.Domain.Models;

namespace CwkBooking.Api.Automapper
{
    public class ReservationMappingProfiles : Profile
    {
        public ReservationMappingProfiles()
        {
            CreateMap<ReservationPutPostDTO, Reservation>();
        }
    }
}
