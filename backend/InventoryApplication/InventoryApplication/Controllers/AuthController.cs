using InventoryApplication.DTO;
using InventoryApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(UserDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("generate-hash")]
        public IActionResult GenerateHash()
        {
            string hash = BCrypt.Net.BCrypt.HashPassword("Admin@123");

            return Ok(hash);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LogInDto dto)
        {
            var user = await _context.UserTbls.FirstOrDefaultAsync(x => x.Email == dto.Email);
            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }
            bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!passwordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }

            var token = GenerateToken(user);
            return Ok(new
            {
                token = token,
                userId = user.UserId,
                userName = user.UserName,
                email = user.Email,
                role = user.Role
            });
        }

          private string GenerateToken(UserTbl user)
        {
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier, user.UserId.ToString()
                    ),

              new Claim(ClaimTypes.Name, user.UserName),
              new Claim (ClaimTypes.Email, user.Email),
              new Claim(ClaimTypes.Role, user.Role)
        }; 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );  
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
