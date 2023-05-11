

using CoronaManageHMO.Controllers;
using CoronaManageHMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CoronaManageHMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : Controller
    {
        private readonly CoronaManageDbContext dbContext;

        public PatientsController(CoronaManageDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetPatient()
        {
            return Ok(await dbContext.Patients.ToListAsync());

        }

        [HttpGet]
        [Route("{MemberId:int}/{PatientNum:int}")]
        public async Task<IActionResult> GetMember([FromRoute] int MemberId, [FromRoute] int PatientNum)
        {
            var member = await dbContext.Patients.FindAsync(MemberId, PatientNum);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> AddPatient(AddPatientRequest addPatientRequest)
        {

            var patient = new Patient
            {
                
                MemberId = addPatientRequest.MemberId,
                PatientNum = addPatientRequest.PatientNum,
                Positive = addPatientRequest.Positive,
                Negative = addPatientRequest.Negative,
            };
            if (!(InputValidity.IsValidDateAndId(patient))
                || !InputValidity.IsPatientInputValid(patient,dbContext))
             
                return BadRequest("Invalid data");


            await dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();
            return Ok(patient);

        }
    }
}
