using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;

        public AuthenticationController(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] ApiUser apiUser)
        {
            var user = AuthenticationValidate(apiUser);

            if (user == null) return NotFound("Kullanıcı Bulunamadı.");

            var token = CreateToken(user);

            return Ok(token);
        }

        private ApiUser? AuthenticationValidate(ApiUser apiUser)
        {
            return ApiUsers.Users
                    .FirstOrDefault(user => user.Username == apiUser.Username
                                    && user.Password == apiUser.Password);
        }

        private string CreateToken(ApiUser user)
        {
            if (jwtSettings.Key == null) throw new ArgumentNullException(nameof(jwtSettings.Key));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimValues = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username!),
                new Claim(ClaimTypes.Role, user.Role!)
            };

            var token = new JwtSecurityToken(jwtSettings.Issuer,
                                                jwtSettings.Audience,
                                                claimValues,
                                                expires: DateTime.Now.AddDays(1),
                                                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
