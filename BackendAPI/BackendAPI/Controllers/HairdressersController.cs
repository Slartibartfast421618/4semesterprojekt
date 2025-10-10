using BackendAPI.Data;
using BackendAPI.DTO;
using BackendAPI.Models;
using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HairdressersController : ControllerBase
    {
        private readonly MaMaDbContext _context;
        private readonly IGeocodingService _geocodingService;

        public HairdressersController(MaMaDbContext context, IGeocodingService geocodingService)
        {
            _context = context;
            _geocodingService = geocodingService;
        }

        // Get All Hairdressers in the DB
        [HttpGet]
        public async Task <ActionResult<IEnumerable<HairdresserDTO>>> GetAllHairdressers()
        {
           // var hairdressers = new List<Hairdresser>();
            var hairdressers = await _context.Hairdressers.ToListAsync();
            var hairdressersDTO = hairdressers.Select(x => new HairdresserDTO
            {
                ID = x.ID , SalonName = x.SalonName, Website = x.Website, Lat = x.Lat , Lng = x.Lng
            }).ToList();
            return Ok(hairdressersDTO);
        }

        // Get coordinates for address input 
        [HttpPost , Route("getCoordinates")]
        public async Task<CoordinatesDTO> Coordinats ([FromQuery] string address)
        {
            CoordinatesDTO addressToCoordinates = new CoordinatesDTO();
            addressToCoordinates = await _geocodingService.GetCoordinatesAsync(address);

            return addressToCoordinates;
        }

        // adresse ind, koordinater + hairdressers ud
        // brug searchDTO som svar 
        // HTTPpost
        // kald Coordinats metode ovenfor, i stedet for at skrive hele kaldet igen ?????? genbrug af kode, men jeg ved ikke hvordan 
    }
}
