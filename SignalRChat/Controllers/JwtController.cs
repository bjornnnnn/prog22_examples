using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalRChat.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public JwtController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet(Name = "CreateToken")]
        public string CreateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "test"),
                    new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                                       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                                                          SecurityAlgorithms.HmacSha256Signature),
                Audience = configuration["Jwt:Audience"],
                Issuer = configuration["Jwt:Issuer"]
            };
            //return Ok(tokenHandler.WriteToken(tokenHandler.CreateToken(securityTokenDescriptor)));
            return tokenHandler.WriteToken(tokenHandler.CreateToken(securityTokenDescriptor));
        }
    }
}
