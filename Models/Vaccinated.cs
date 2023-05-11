using System;
using System.Collections.Generic;

namespace CoronaManageHMO.Models;

public partial class Vaccinated
{
    public int MemberId { get; set; }

    public int VaccinationId { get; set; }

    public DateTime VaccinationDate { get; set; }

    public virtual Member Member { get; set; } = null!;
}
