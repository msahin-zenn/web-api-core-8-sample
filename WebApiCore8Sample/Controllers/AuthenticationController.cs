using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using WebApiCore8Sample.Dtos;
using WebApiCore8Sample.Models;
using WebApiCore8Sample.Services;

namespace WebApiCore8Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthenticationController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register([FromBody] ApiUserRequestDto apiUserDto)
        {
            var response = await authService.Register(new ApiUser { Username = apiUserDto.Username }, apiUserDto.Password);

            if (response.Status == false)
            {
                if (response.StatusCode == (int)HttpStatusCode.NotFound) return NotFound(response);
                if (response.StatusCode == (int)HttpStatusCode.BadRequest) return BadRequest(response);
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login([FromBody] ApiUserRequestDto apiUserDto)
        {
            var response = await authService.Login(apiUserDto.Username, apiUserDto.Password);

            if (response.Status == false)
            {
                if (response.StatusCode == (int)HttpStatusCode.NotFound) return NotFound(response);
                if (response.StatusCode == (int)HttpStatusCode.BadRequest) return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
