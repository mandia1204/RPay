using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace RPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        IConfiguration _config;
        public SecurityController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("token")]
        public string GetToken()
        {
            var issuer = _config["TokenSettings:Issuer"];
            var audience = _config["TokenSettings:Audience"];
            var key = Encoding.ASCII.GetBytes(_config["TokenSettings:SigningKey"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("TestClaim", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("TokenSettings:ExpiresInMinutes")),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
