
using CoronaManageHMO.Controllers;
using CoronaManageHMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoronaManageHMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccinationController : Controller
    {
        private readonly CoronaManageDbContext dbContext;
        public VaccinationController(CoronaManageDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetVaccination()
        {
            return Ok(await dbContext.Vaccinations.ToListAsync());

        }

        [HttpGet]
        [Route("{VaccinationId:int}")]
        public async Task<IActionResult> GetVaccination([FromRoute] int VaccinationId)
        {
            var vaccination = await dbContext.Vaccinations.FindAsync(VaccinationId);
            if (vaccination == null)
            {
                return NotFound();
            }
            return Ok(vaccination);
        }

        [HttpPost]
        public async Task<IActionResult> AddVaccination(AddVaccinationRequest addVaccinationRequest)
        {
            var vaccination = new Vaccination
            {
                VaccinationId = addVaccinationRequest.VaccinationId,
                Manufacturer = addVaccinationRequest.Manufacturer,
            };
            if (!(InputValidity.IsValidVaccination(vaccination)))
                return BadRequest("Invalid data");
           

                await dbContext.Vaccinations.AddAsync(vaccination);
                await dbContext.SaveChangesAsync();
                return Ok(vaccination);
           
        }
    }
}
