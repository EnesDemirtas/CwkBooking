using AutoMapper;
using CwkBooking.Api.DTOs;
using CwkBooking.Domain.Abstracts.Services;
using CwkBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
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
            var result = await _reservationsService.MakeReservation(reservation);

            if (result == null)
                return BadRequest("Cannot create reservation");

            return Ok(result);
        }
    }
}
