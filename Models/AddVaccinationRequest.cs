namespace CoronaManageHMO.Models
{
    public class AddVaccinationRequest
    {
        public int VaccinationId { get; set; }

        public string Manufacturer { get; set; } = null!;
    }
}
