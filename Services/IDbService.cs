using System.Collections.Generic;
using System.Threading.Tasks;
using APBD8.Models;

namespace APBD8.Services
{
    public interface IDbService
    {
        Task<object> GetDoctor(int idDoctor);
        Task AddDoctor(Doctor doctor);
        Task DeleteDoctor(int idDoctor);
        Task ModifyDoctor(Doctor doctor, int idDoctor);
        Task<object> GetPrescription(int idPrescription);
        Task<Account> Login(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
    }
}
