using System;
using System.Collections.Generic;

namespace CoronaManageHMO.Models;

public partial class Patient
{
    public int MemberId { get; set; }

    public int PatientNum { get; set; }

    public DateTime Positive { get; set; }

    public DateTime Negative { get; set; }

    public virtual Member Member { get; set; } = null!;
}
