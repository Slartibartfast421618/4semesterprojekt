using BackendAPI.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HairdressersController : ControllerBase
    {
        private readonly MaMaDbContext _context;

        public HairdressersController(MaMaDbContext context)
        {
            _context = context;
        }

        // Get All Hairdressers in the DB
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Hairdresser>>> GetAllHairdressers()
        {
            var hairdressers = new List<Hairdresser>();
            hairdressers = await _context.Hairdressers.ToListAsync();
            return Ok(hairdressers);
        }
    }
}
