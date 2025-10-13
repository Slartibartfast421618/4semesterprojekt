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
        private readonly ITreatmentService _treatmentService;

        public HairdressersController(MaMaDbContext context, 
            IGeocodingService geocodingService, 
            ITreatmentService treatmentService)
        {
            _context = context;
            _geocodingService = geocodingService;
            _treatmentService = treatmentService;
        }

        // Get All Hairdressers in the DB
        [HttpGet , Route("AllHairdressers")]
        public async Task <ActionResult<IEnumerable<HairdresserDTO>>> GetAllHairdressers()
        {
            List<Hairdresser> hairdressers = await _context.Hairdressers.ToListAsync();
            List<HairdresserDTO> hairdressersDTO = hairdressers.Select(x => new HairdresserDTO
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

        // Get coordinates, hairdressers, treatments(+ price)  
        [HttpPost , Route("Search")]
        public async Task<SearchDTO> GetCoordinatsHairdressersTreatments([FromQuery] string address)
        {
            // convert address to coodinats
            CoordinatesDTO coordinats = await Coordinats(address);

            // converte to range/set distance

            // get hairdressers for DB
            //// for now - get all 
            // get treatments and prices - needs to be able to scrape internet directly or a new DB to hold the data 
            //// for now - dummy randomized data
            List<Hairdresser> hairdressers = await _context.Hairdressers.ToListAsync();
            List<HairdresserWithTreatmentsDTO> hairdressersWithTreatmentsDTOs = hairdressers.Select(x => new HairdresserWithTreatmentsDTO
            {
                ID = x.ID,
                SalonName = x.SalonName,
                Website = x.Website,
                Lat = x.Lat,
                Lng = x.Lng,
                Treatments = _treatmentService.GetRandomDummyTreatments()
            }).ToList();

            return new SearchDTO
            {
                coordinates = coordinats, 
                hairdressers = hairdressersWithTreatmentsDTOs
            }; 
        }
    }
}
