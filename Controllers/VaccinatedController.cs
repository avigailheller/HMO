using CoronaManageHMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace CoronaManageHMO.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class VaccinatedController : Controller
    {
        private readonly CoronaManageDbContext dbContext;
        public VaccinatedController(CoronaManageDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            return Ok(await dbContext.Members.ToListAsync());

        }

        [HttpGet]
        [Route("{MemberId:int}/{VaccinationId:int}")]
        public async Task<IActionResult> GetVaccinated([FromRoute] int MemberId, [FromRoute] int VaccinationId)
        {
            var vaccinated = await dbContext.Vaccinated.FindAsync(MemberId, VaccinationId);
            if (vaccinated == null)
            {
                return NotFound();
            }
            return Ok(vaccinated);
        }

        [HttpPost]
        public async Task<IActionResult> AddVaccinated(AddVaccinatedRequest addVaccinatedRequest)
        {
            if (!InputValidity.IsValidVaccinatedTimes(addVaccinatedRequest, dbContext))
            {
                return BadRequest("Invalid Input");
            }
            var vaccinated = new Vaccinated
            {
                MemberId = addVaccinatedRequest.MemberId,
                VaccinationId = addVaccinatedRequest.VaccinationId,
                VaccinationDate = addVaccinatedRequest.VaccinationDate,
            };
            if (!(InputValidity.IsValidVaccinated(vaccinated)))
                
                return BadRequest("Invalid Input");
           

                await dbContext.Vaccinated.AddAsync(vaccinated);
                await dbContext.SaveChangesAsync();
                return Ok(vaccinated);
           
        }


    }
}
