using AutoMapper;
using CwkBooking.Api.DTOs;
using CwkBooking.Domain.Models;

namespace CwkBooking.Api.Automapper
{
    public class HotelMappingProfiles : Profile
    {
        public HotelMappingProfiles()
        {
            CreateMap<HotelCreateDTO, Hotel>();
            CreateMap<Hotel, HotelGetDTO>();
        }
    }
}
