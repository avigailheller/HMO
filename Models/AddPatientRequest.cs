namespace CoronaManageHMO.Models
{
    public class AddPatientRequest
    {
        public int MemberId { get; set; }
        public int PatientNum { get; set; }
        public DateTime Positive { get; set; }
        public DateTime Negative { get; set; }
    }
}
