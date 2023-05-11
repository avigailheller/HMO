namespace CoronaManageHMO.Models
{
    public class AddVaccinatedRequest
    {
        public int MemberId { get; set; }

        public int VaccinationId { get; set; }

        public DateTime VaccinationDate { get; set; }

    }
}

