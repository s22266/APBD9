using System.Collections.Generic;
using System.Threading.Tasks;
using APBD8.Models;
using APBD8.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD8.Controllers
{
    [Route("api/hospital")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly IDbService _dbService;
        public Controller(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("secret")]
        [Route("{idDoctor}")]
        [Authorize]
        public async Task<object> GetDoctor(int idDoctor)
        {
            return await _dbService.GetDoctor(idDoctor);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDoctor(Doctor doctor)
        {
            return Ok("Doctor has been added");
        }

        [HttpDelete]
        [Route("{idDoctor}")]
        [Authorize]
        public async Task<IActionResult> DeleteDoctor(int idDoctor)
        {
            await _dbService.DeleteDoctor(idDoctor);
            return Ok("Doctor has been deleted");
        }

        [HttpPost]
        [Route("{idDoctor}/modifyDoctor")]
        [Authorize]
        public async Task<IActionResult> ModifyDoctor(Doctor doctor, int idDoctor) 
        { 
            await _dbService.ModifyDoctor(doctor, idDoctor);
            return Ok("Doctor has been modified");
        }

        [HttpGet]
        [Route("{idPrescription}/prescription")]
        [Authorize]
        public async Task<object> GetPrescription(int idPrescription) 
        {
            return await _dbService.GetPrescription(idPrescription);
        }

    }
}
