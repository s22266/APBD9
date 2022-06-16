using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using APBD8.Models;
using APBD8.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace APBD8.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase

    {

        private readonly IDbService _dbService;
        public AccountsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginRequest request) {

            var checkAccount = await _dbService.Login(request);
           

            var hasher = new PasswordHasher<Account>();
            var hashedPassword = hasher.VerifyHashedPassword(checkAccount, checkAccount.Password, request.Password);

            if (hashedPassword == PasswordVerificationResult.Success)
            {
                var options = BuildToken(checkAccount);
                var refreshTokenOptions = BuildRefreshToken(checkAccount);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(options),
                    refreshToken = refreshTokenOptions
                });
            }
            else 
            {
                return BadRequest("Wrong login or password");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request) 
        { 
            if(request.Login == null || request.Password == null)
            {
                return BadRequest();
            }
            await _dbService.Register(request);
            return Ok("Account has been registered");
        }

        private JwtSecurityToken BuildToken(Account account)
        {
            var claims = new Claim[]
            {
            new(ClaimTypes.NameIdentifier, account.Login)
            };

            string secret = "4f37f438f349f4fdnsnd300gfdq31AD@824lfn34f3ofv3if2if74f30g5j34uveubciec";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("http://localhost:5000", "http://localhost:5000", claims, expires: DateTime.UtcNow.AddMinutes(5));
            
            return token;
        }

        private string BuildRefreshToken(Account account)
        {
            var refreshToken = "";
            using (var genNum = RandomNumberGenerator.Create()) 
            {
                var r = new byte[1024];
                genNum.GetBytes(r);
                refreshToken = Convert.ToBase64String(r);
            }

            return refreshToken;
        }
    }

}
