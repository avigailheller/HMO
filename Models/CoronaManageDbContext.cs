using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoronaManageHMO.Models;

public partial class CoronaManageDbContext : DbContext
{
  
    public CoronaManageDbContext(DbContextOptions<CoronaManageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Vaccinated> Vaccinated { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("memberId");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("dateOfBirth");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("fullName");
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(50)
                .HasColumnName("mobilePhone");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.PatientNum });

            entity.Property(e => e.MemberId).HasColumnName("memberId");
            entity.Property(e => e.PatientNum).HasColumnName("patientNum");
            entity.Property(e => e.Negative)
                .HasColumnType("date")
                .HasColumnName("negative");
            entity.Property(e => e.Positive)
                .HasColumnType("date")
                .HasColumnName("positive");

            entity.HasOne(d => d.Member).WithMany(p => p.Patients)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patients_Members");
        });

        modelBuilder.Entity<Vaccinated>(entity =>
        {
            entity.HasKey(e => new { e.MemberId, e.VaccinationId });

            entity.ToTable("Vaccinated");

            entity.Property(e => e.MemberId).HasColumnName("memberId");
            entity.Property(e => e.VaccinationId).HasColumnName("vaccinationId");
            entity.Property(e => e.VaccinationDate)
                .HasColumnType("date")
                .HasColumnName("vaccinationDate");

            entity.HasOne(d => d.Member).WithMany(p => p.Vaccinateds)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vaccinated_Members");
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.ToTable("Vaccination");

            entity.Property(e => e.VaccinationId)
                .ValueGeneratedNever()
                .HasColumnName("vaccinationId");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(50)
                .HasColumnName("manufacturer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
