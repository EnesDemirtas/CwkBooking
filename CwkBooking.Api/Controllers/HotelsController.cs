using AutoMapper;
using CwkBooking.Api.DTOs;
using CwkBooking.Domain.Abstracts.Repositories;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CwkBooking.Api.Controllers
{

    // /hotels
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly IHotelsRepository _hotelsRepo;
        private readonly IMapper _mapper;
        public HotelsController(IHotelsRepository repo, IMapper mapper)
        {
            _hotelsRepo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelsRepo.GetAllHotelsAsync();
            var hotelsGet = _mapper.Map<List<HotelGetDTO>>(hotels);
            return Ok(hotelsGet);
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelsRepo.GetHotelByIdAsync(id);

            if (hotel == null)
                return NotFound();

            var hotelGet = _mapper.Map<HotelGetDTO>(hotel);
            return Ok(hotelGet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDTO hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);

            await _hotelsRepo.CreateHotelAsync(domainHotel);

            var hotelGet = _mapper.Map<HotelGetDTO>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new {id = domainHotel.HotelId}, hotelGet);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelCreateDTO updated, int id)
        {
            var toUpdate = _mapper.Map<Hotel>(updated);
            toUpdate.HotelId = id;
               
            await _hotelsRepo.UpdateHotelAsync(toUpdate);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepo.DeleteHotelAsync(id);

            if (hotel == null)
                return NotFound();


            return NoContent();
        }

        [HttpGet]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> GetAllHotelRooms(int hotelId)
        {
            var rooms = await _hotelsRepo.ListHotelRoomAsync(hotelId);
            var mappedRooms = _mapper.Map<List<RoomGetDTO>>(rooms);
            return Ok(mappedRooms);
        }

        [HttpGet]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> GetHotelRoomById(int hotelId, int roomId)
        {
            var room = await _hotelsRepo.GetHotelRoomByIdAsync(hotelId, roomId);
            var mappedRoom = _mapper.Map<List<RoomGetDTO>>(room);

            return Ok(mappedRoom);
        }

        [HttpPost]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> AddHotelRoom(int hotelId, [FromBody] RoomPostPutDTO newRoom)
        {
            var room = _mapper.Map<Room>(newRoom);
            await _hotelsRepo.CreateHotelRoomAsync(hotelId, room);

            var mappedRoom = _mapper.Map<RoomGetDTO>(room);

            return CreatedAtAction(nameof(GetHotelRoomById), new {hotelId = hotelId, roomId = mappedRoom.RoomId}, mappedRoom);
        }

        [HttpPut]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateHotelRoom(int hotelId, int roomId, [FromBody] RoomPostPutDTO updatedRoom)
        {
            var toUpdate = _mapper.Map<Room>(updatedRoom);
            toUpdate.RoomId = roomId;
            toUpdate.HotelId = hotelId;

            await _hotelsRepo.UpdateHotelRoomAsync(hotelId, toUpdate);

            return NoContent();
        }

        [HttpDelete]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> RemoveRoomFromHotel(int hotelId, int roomId)
        {
            var room = await _hotelsRepo.DeleteHotelRoomAsync(hotelId, roomId);
            if (room == null)
                return NotFound("Room not found");

            return NoContent();
        }
    }
}
