using CoronaManageHMO.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CoronaManageHMO.Controllers
{
    public static class InputValidity
    {


        public static bool IsValidMember(Member member)
        {
            if (string.IsNullOrWhiteSpace(member.FullName) ||
                string.IsNullOrWhiteSpace(member.Street) ||
                string.IsNullOrWhiteSpace(member.City) ||
                string.IsNullOrWhiteSpace(member.Country) ||
                member.DateOfBirth == DateTime.MinValue ||
                string.IsNullOrWhiteSpace(member.Phone) ||
                string.IsNullOrWhiteSpace(member.MobilePhone))
            {
                return false;
            }

            // Check if the phone number is in the correct format
            if (!Regex.IsMatch(member.Phone, @"^\d{3}\-\d{3}\-\d{4}$")
                || !Regex.IsMatch(member.FullName, @"^[a-zA-Z\s\-]+$")
                ||  !Regex.IsMatch(member.Street, @"^[a-zA-Z\s\,\-]+$")
                ||  !Regex.IsMatch(member.City, @"^[a-zA-Z\s\,\-]+$")
                || !Regex.IsMatch(member.Country, @"^[a-zA-Z\s\,\-]+$")
                || !Regex.IsMatch(member.MobilePhone, @"^\d{3}\-\d{3}\-\d{4}$")
                || !(member.DateOfBirth <= DateTime.Today))
            {
                return false;
            }

            // Check if the mobile phone number is in the correct format
           // if (!Regex.IsMatch(member.MobilePhone, @"^\d{3}\-\d{3}\-\d{4}$"))
           // {
           //     return false;
           // }

            return true;
        }
       // public static bool IsValid(Member member)
       // {
          //  return Regex.IsMatch(member.FullName, @"^[a-zA-Z\s\-]+$")
           //      && Regex.IsMatch(member.MemberId.ToString(), @"^\d{9}$")
            //     && Regex.IsMatch(member.Street, @"^[a-zA-Z0-9\s\,\-]+$")
             //    && Regex.IsMatch(member.City, @"^[a-zA-Z0-9\s\,\-]+$")
              //   && Regex.IsMatch(member.Country, @"^[a-zA-Z0-9\s\,\-]+$")
              //   && Regex.IsMatch(member.Phone, @"^\d{3}\-\d{3}\-\d{4}$")
              //   && Regex.IsMatch(member.MobilePhone, @"^\+(?:[0-9] ?){6,14}[0-9]$")
              //   && member.DateOfBirth <= DateTime.Today;
        //}
        public static bool IsValidDateAndId(Patient patient)
        {
            return patient.Positive < patient.Negative
                && patient.Negative <= DateTime.Today
                //&& patient.MemberId == patient.Member.MemberId
                && !string.IsNullOrEmpty(patient.MemberId.ToString());
               // && Regex.IsMatch(patient.MemberId.ToString(), @"^\d{9}$");
               
        }



        public static bool IsPatientInputValid(Patient patient, CoronaManageDbContext dbContext)
        {
            var existingPatient = dbContext.Patients.FirstOrDefault(v => v.MemberId == patient.MemberId);
            if (existingPatient != null)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidVaccinated(Vaccinated vaccinated)
        {
            return //vaccinated.MemberId == vaccinated.Member.MemberId
                 vaccinated.VaccinationId > 0 && vaccinated.VaccinationId <= 4
                && vaccinated.VaccinationDate <= DateTime.Now;
        }
        public static bool IsValidVaccination(Vaccination vaccination)
        {
            return !string.IsNullOrEmpty(vaccination.Manufacturer)
                 && vaccination.VaccinationId > 0 && vaccination.VaccinationId <= 4;
        }
        public static bool IsValidVaccinatedTimes(AddVaccinatedRequest inputV, CoronaManageDbContext dbContext)
        {

            if (dbContext.Vaccinated.Count(pv => pv.MemberId == inputV.MemberId) >= 4)
            {
                return false;
            }

            return true;
        }
    }
}
