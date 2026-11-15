using DRMusicRecords.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DRMusicRecords.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            string? role = null;

            if (request.Username == "admin" &&
                request.Password == _configuration["DemoUsers:AdminPassword"] &&
                !string.IsNullOrEmpty(_configuration["DemoUsers:AdminPassword"]))
            {
                role = "Admin";
            }
            else if (request.Username == "student" &&
                request.Password == _configuration["DemoUsers:StudentPassword"] &&
                !string.IsNullOrEmpty(_configuration["DemoUsers:StudentPassword"]))
            {
                role = "User";
            }

            if (role == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                username = request.Username,
                role = role
            });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            return Ok(new
            {
                username = User.Identity?.Name,
                role = User.FindFirst(ClaimTypes.Role)?.Value
            });
        }
    }
}
