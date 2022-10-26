using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StarWars.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StarWars.WebApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IConfiguration _configuration;
       
        public AuthenticationController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserModel user)
        {
            if (user.UserName != null && user.Password != null)
            {
                if (user.UserName == "admin" && user.Password == "admin")
                {
                    
                    return Ok(CreateJwtToken(user));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest("");
            }
        }

        private string CreateJwtToken(UserModel user)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var credentials = new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            );

            var userCliams = new Claim[]{
                new Claim("UserName", user.UserName)
            };

            var jwtToken = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               expires: DateTime.Now.AddMinutes(20),
               signingCredentials: credentials,
               claims: userCliams,
               audience: _configuration["Jwt:Audience"]
           );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}