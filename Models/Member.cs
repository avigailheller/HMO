using System;
using System.Collections.Generic;

namespace CoronaManageHMO.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Phone { get; set; } = null!;

    public string MobilePhone { get; set; } = null!;
    public byte[]? Photo { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<Vaccinated> Vaccinateds { get; set; } = new List<Vaccinated>();
}
