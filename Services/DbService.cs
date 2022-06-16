using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD8.DataAccess;
using APBD8.Models;
using APBD8.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APBD8.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _dbContext;
        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddDoctor(Doctor doctor)
        {
            var d = new Doctor()
            {
                IdDoctor = doctor.IdDoctor,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            _dbContext.Add(d);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDoctor(int idDoctor)
        {
            var exists = await _dbContext.Doctors.AnyAsync(d => d.IdDoctor == idDoctor);
            if (exists)
            {
                var doctor = _dbContext.Doctors.Where(d => d.IdDoctor.Equals(idDoctor)).First();

                _dbContext.Attach(doctor);
                _dbContext.Remove(doctor);

                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<object> GetDoctor(int idDoctor)
        {
            return await _dbContext.Doctors.Where(d => d.IdDoctor == idDoctor).FirstAsync();
        }

        public async Task ModifyDoctor(Doctor doctor, int idDoctor)
        {
            var d = await _dbContext.Doctors.Where(d => d.IdDoctor == idDoctor).FirstOrDefaultAsync();

            d.FirstName = doctor.FirstName;
            d.LastName = doctor.LastName;
            d.Email = doctor.Email;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<object> GetPrescription(int idPrescription) 
        { 
            return await _dbContext.Prescriptions.Include(p => p.Patient)
                                                 .Include(d => d.Doctor)
                                                 .Include(m => m.Prescription_Medicaments)
                                                 .Where(e => e.IdPrescription == idPrescription)
                                                 .Select(e => new SomeSortOfPrescripton 
                                                 { 
                                                    IdPrescription = e.IdPrescription,
                                                    Date = e.Date,
                                                    DueDate = e.DueDate,
                                                    Patient = e.Patient,
                                                    Doctor = e.Doctor,
                                                    Medicaments = e.Prescription_Medicaments.Select(m => m.Medicament).ToList()
                                                 }).ToListAsync();
        }

        public async Task<Account> Login(LoginRequest request)
        {
            return await _dbContext.Accounts.SingleOrDefaultAsync(a => a.Login.Equals(request.Login));
        }

        public async Task Register(RegisterRequest request)
        {
            var a = new Account 
            { 
                Login = request.Login,
                Password = request.Password
            };

            var hasher = new PasswordHasher<Account>();
            var hashedPassword = hasher.HashPassword(a, a.Password);

            a.Password = hashedPassword;

            _dbContext.Attach(a);
            _dbContext.SaveChangesAsync();

        }
    }
}
