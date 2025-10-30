using BackendAPI.Data;
using BackendAPI.DTO;
using BackendAPI.Models;
using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

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

            // get hairdressers for DB
            //// get within 10 km radius from input address 
            List<Hairdresser> hairdressers = new();
            if (double.TryParse(coordinats.Lat, CultureInfo.InvariantCulture , out double latitude) && 
                double.TryParse(coordinats.Lng, CultureInfo.InvariantCulture , out double longtitude))
            {
                hairdressers = await _context.Hairdressers.FromSqlRaw(@"
                    SELECT * FROM ""Hairdressers""
                    WHERE ST_DWithin(   geography(ST_MakePoint(""Lng"", ""Lat"")),
                                        geography(ST_MakePoint({0}, {1})),
                    {2} * 1000)", longtitude, latitude, 10).ToListAsync();
            }
            else
            {
                // Get all hairdressers, så the list is full - TEMPORARY
                    // not a good solution with a database with many datapoints 
                hairdressers = await _context.Hairdressers.ToListAsync();
                // alternative 
                //hairdressers = null; 
            }

            // convert List<Hairdresser> to List<HairdresserWithTreatmentDTO> and get tratments + prices           
            List<HairdresserWithTreatmentsDTO> hairdressersWithTreatmentsDTOs = hairdressers.Select(x => new HairdresserWithTreatmentsDTO
                {
                    ID = x.ID,
                    SalonName = x.SalonName,
                    Website = x.Website,
                    Lat = x.Lat,
                    Lng = x.Lng,
                    // get treatments and prices - needs to be able to scrape internet directly or a new DB to hold the data 
                    //// for now - dummy randomized data -> long term being able to scrape or get the apropriate data from the db (if saved)
                    Treatments = _treatmentService.GetRandomDummyTreatments()
                }).ToList();

            return new SearchDTO
            {
                coordinates = coordinats, 
                hairdressers = hairdressersWithTreatmentsDTOs
            }; 
        }

        // Add hairdressers to DB
        [HttpPost]
        public async Task<ActionResult> AddHairdressers([FromBody] HairdresserAddDTO addDTO)
        {
            // validation of input (addDTO)
            if(!ModelState.IsValid) return BadRequest(ModelState);

            else
            {
                // convert adress to coordinats
                CoordinatesDTO coordinats = await Coordinats(addDTO.Address);

                // create hairdresser object
                var hairdresser = new Hairdresser
                {
                    SalonName = addDTO.SalonName,
                    Website = addDTO.Website,
                    Lat = double.Parse(coordinats.Lat, CultureInfo.InvariantCulture), // without try - missing som error handling 
                    Lng = double.Parse(coordinats.Lng, CultureInfo.InvariantCulture)
                };

                // send hairdresserobjekt to DB
                _context.Hairdressers.Add(hairdresser); // input hairdresser to local DbContext memory
                await _context.SaveChangesAsync(); // sending changes to DB

                // respons to client "created"
                return StatusCode(201);
            }
        }
    }
}
