using AutoMapper;
using CwkBooking.Api.DTOs;
using CwkBooking.Domain.Models;

namespace CwkBooking.Api.Automapper
{
    public class RoomMappingProfiles : Profile
    {
        public RoomMappingProfiles()
        {
            CreateMap<Room, RoomGetDTO>();
            CreateMap<RoomPostPutDTO, Room>();
        }
    }
}
