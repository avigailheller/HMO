using CoronaManageHMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoronaManageHMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonusController : Controller
    {
        private readonly CoronaManageDbContext dbContext;

        public BonusController(CoronaManageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("unvaccinated-members")]
        public IActionResult GetUnvaccinatedCopaMembers()
        {
            var unvaccinatedCount = dbContext.Members
                .Where(p => p.Vaccinateds.Count == 0)
                .Count();

            return Ok(unvaccinatedCount);
        }


        [HttpGet("active-patients-last-month")]
        public async Task<ActionResult<List<int?>>> GetActivePatientsLastMonth()
        {
            var endDate = DateTime.Today.Date;
            var startDate = endDate.AddDays(-30).Date;

            var activePatientsLastMonth = new List<int?>();

            while (startDate <= endDate)
            {
                var activePatients = await dbContext.Patients
                    .CountAsync(p => p.Positive <= startDate && p.Negative >= startDate);

                activePatientsLastMonth.Add(activePatients);
                startDate = startDate.AddDays(1);
            }

            return activePatientsLastMonth;
        }



    }
}
