using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiCore8Sample.Dtos;

namespace WebApiCore8Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,PowerUser")]
    public class ApiUserController : ControllerBase
    {
        // GET: api/<ApiUserController>
        [HttpGet]
        public ActionResult<ApiUserResponseDto> Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null) return NotFound();

            var user = new ApiUserResponseDto();
            user.Id = new Random().Next(1000,9999);
            user.Username = identity.Claims.FirstOrDefault(item => item.Type == ClaimTypes.NameIdentifier)?.Value;
            user.Role = identity.Claims.FirstOrDefault(item => item.Type == ClaimTypes.Role)?.Value;

            return Ok(user);
        }

        // GET api/<ApiUserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<ApiUserController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<ApiUserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<ApiUserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
