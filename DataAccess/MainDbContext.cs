using System;
using APBD8.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD8.DataAccess
{
    public class MainDbContext : DbContext
    {
        protected MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions options) : base(options)
        { 
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=master;Persist Security Info=True;User ID = SA;Password=<YourStrong@Passw0rd>");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.BirthDate).IsRequired();

                p.HasData(
                    new Patient { IdPatient = 1, FirstName = "Andrzej", LastName = "Nowak", BirthDate = DateTime.Parse("1966-07-01") },
                    new Patient { IdPatient = 2, FirstName = "Ewa", LastName = "Wysoka", BirthDate = DateTime.Parse("1999-12-09") },
                    new Patient { IdPatient = 3, FirstName = "Robert", LastName = "Lewandowski", BirthDate = DateTime.Parse("2000-01-01") }
                    );
            });

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);

                d.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Mateusz", LastName = "Kozak", Email = "mati@email.com" },
                    new Doctor { IdDoctor = 2, FirstName = "Robert", LastName = "Rakieta", Email = "robert@yahoo.com" }
                    );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();

                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Parse("2021-12-12"), DueDate = DateTime.Parse("2022-01-01"), IdDoctor = 1, IdPatient = 1 },
                    new Prescription { IdPrescription = 2, Date = DateTime.Parse("2021-12-12"), DueDate = DateTime.Parse("2022-01-01"), IdDoctor = 2, IdPatient = 2 }
                    );
            });

            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);

                m.HasData(
                    new Medicament { IdMedicament = 1, Name = "Ibuprom", Description = "Na bol glowy", Type = "Doustny" },
                    new Medicament { IdMedicament = 2, Name = "Apap", Description = "Na bol stawow", Type = "Doustny" },
                    new Medicament { IdMedicament = 3, Name = "Marsjanki", Description = "Na wszystko", Type = "Doustny" }
                    );

            });

            modelBuilder.Entity<Prescription_Medicament>(m =>
            {
                m.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                m.Property(e => e.Dose);
                m.Property(e => e.Details).IsRequired().HasMaxLength(100);

                m.HasOne(e => e.Medicament).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdMedicament);
                m.HasOne(e => e.Prescription).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdPrescription);

                m.HasData(
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "details" },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = 100, Details = "details" }
                    );
            });

            modelBuilder.Entity<Account>(a => 
            { 
                a.HasKey(e => e.Login);
                a.Property(e => e.Password);
                a.Property(e => e.RefreshToken);

                a.HasData(
                    new Account { Login = "User", Password = "User", RefreshToken = "247fh249f429f10330d298ff43f"},
                    new Account { Login = "User2", Password = "User2", RefreshToken = "4vn3v3409v89YSN48f40ffnb40f" }
                );
            });
        }
    }
}
