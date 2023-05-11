using System;
using System.Collections.Generic;

namespace CoronaManageHMO.Models;

public partial class Vaccination
{
    public int VaccinationId { get; set; }

    public string Manufacturer { get; set; } = null!;
}
