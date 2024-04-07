using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiCore8Sample.Data;
using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext context;
        private readonly JwtSettings jwtSettings;

        public AuthService(DataContext context, IOptions<JwtSettings> jwtSettings)
        {
            this.context = context;
            this.jwtSettings = jwtSettings.Value;
        }

        public async Task<ServiceResponse<int>> Register(ApiUser user, string password)
        {
            var response = new ServiceResponse<int>();

            if (await UserExists(user.Username) == true)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Detail = "User already exists.";
                return response;
            }

            PasswordHashCompute(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.ApiUsers.Add(user);
            await context.SaveChangesAsync();

            response.Data = user.Id;

            return response;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();

            var user = await context.ApiUsers.FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());

            if (user == null)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Detail = "Not found.";
            }
            else if (PasswordHashValidate(password, user.PasswordHash, user.PasswordSalt) == false)
            {
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Detail = "Username or password not matched.";
            }
            else
            {
                var token = SecurityTokenGenerate(user);

                response.Data = token;
            }

            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await context.ApiUsers.AnyAsync(user => user.Username.ToLower() == username.ToLower()))
            {
                return true;
            }

            return false;
        }

        private void PasswordHashCompute(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool PasswordHashValidate(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                return passwordHash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        private string SecurityTokenGenerate(ApiUser user)
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
