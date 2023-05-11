
using CoronaManageHMO.Controllers;
using CoronaManageHMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoronaManageHMO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : Controller
    {
        private readonly CoronaManageDbContext dbContext;
        public MembersController(CoronaManageDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            return Ok(await dbContext.Members.ToListAsync());

        }
        [HttpGet]
        [Route("{MemberId:int}")]
        public async Task<IActionResult> GetMember([FromRoute] int MemberId)
        {
            var member = await dbContext.Members.FindAsync(MemberId);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }


        [HttpPost]
        public async Task<IActionResult> AddMember(AddMemberRequest addMemberRequest)
        {
            var member = new Member
            {
                MemberId = addMemberRequest.MemberId,
                FullName = addMemberRequest.FullName,
                DateOfBirth = addMemberRequest.DateOfBirth,
                Phone = addMemberRequest.Phone,
                MobilePhone = addMemberRequest.MobilePhone,
                Street = addMemberRequest.Street,
                City = addMemberRequest.City,
                Country = addMemberRequest.Country,
            };


            if (!(InputValidity.IsValidMember(member) ))
                return BadRequest("Invalid data");


            await dbContext.Members.AddAsync(member);
            //await dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Members ON;");
            await dbContext.SaveChangesAsync();
            return Ok(member);
        }


        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile file)
        {
            var member = await dbContext.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                member.Photo = stream.ToArray();
            }

            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/photo")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var member = await dbContext.Members.FindAsync(id);
            if (member == null || member.Photo == null)
            {
                return NotFound();
            }

            return File(member.Photo, "image/jpeg");
        }


       // [HttpGet]
       // [Route("unvaccinated-members")]
       // public IActionResult GetUnvaccinatedMembers()
       // {
        //    string sqlQuery = "SELECT COUNT(m.memberId) - COUNT(v.memberId) as unvaccinated_members FROM Members m LEFT JOIN Vaccinated v ON m.memberId = v.memberId;";

         //   int unvaccinatedMembers = dbContext.Members.FromSqlRaw(sqlQuery).FirstOrDefault().unvaccinated_members;
          //  return Ok(unvaccinatedMembers);
       // }
    }



    }

