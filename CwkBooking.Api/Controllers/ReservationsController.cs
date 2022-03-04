using AutoMapper;
using CwkBooking.Api.DTOs;
using CwkBooking.Domain.Abstracts.Services;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CwkBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationsService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationsService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationPutPostDTO reservationDTO)
        {
            var reservation = _mapper.Map<Reservation>(reservationDTO);
            var result = await _reservationsService.MakeReservationAsync(reservation);

            if (result == null)
                return BadRequest("Cannot create reservation");

            var mapped = _mapper.Map<ReservationGetDTO>(result);

            return Ok(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationsService.GetAllReservationsAsync();
            var mapped = _mapper.Map<List<ReservationGetDTO>>(reservations);

            return Ok(mapped); 
        }

        [HttpGet]
        [Route("{reservationId}")]
        public async Task<IActionResult> GetReservationById(int reservationId)
        {
            var reservation = await _reservationsService.GetReservationByIdAsync(reservationId);
            if (reservation == null)
                return NotFound($"No reservation found for the id: {reservationId}");
            var mapped = _mapper.Map<ReservationGetDTO>(reservation);
            return Ok(mapped);
        }

        [HttpDelete]
        [Route("{reservationId}")]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            var deleted = await _reservationsService.DeleteReservationAsync(reservationId);
            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
