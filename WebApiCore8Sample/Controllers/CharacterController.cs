using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiCore8Sample.Dtos;
using WebApiCore8Sample.Models;
using WebApiCore8Sample.Services.CharacterService;

namespace WebApiCore8Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        // GET: api/<CharacterController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<CharacterGetDto>>> Get()
        {
            var response = await characterService.Get();

            if (response.Status == false)
            {
                if (response.StatusCode == (int)HttpStatusCode.NotFound) return NotFound(response);
                if (response.StatusCode == (int)HttpStatusCode.BadRequest) return BadRequest(response);
            }

            return Ok(response);
        }

        // GET api/<CharacterController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterGetDto>>> Get(int id)
        {
            var response = await characterService.Get(id);

            if (response.Status == false)
            {
                if (response.StatusCode == (int)HttpStatusCode.NotFound) return NotFound(response);
                if (response.StatusCode == (int)HttpStatusCode.BadRequest) return BadRequest(response);
            }

            return Ok(response);
        }

        // POST api/<CharacterController>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<CharacterGetDto>>> Post([FromBody] CharacterAddDto character)
        {
            var response = await characterService.Add(character);

            if (response.Status == false)
            {
                if (response.StatusCode == (int)HttpStatusCode.NotFound) return NotFound(response);
                if (response.StatusCode == (int)HttpStatusCode.BadRequest) return BadRequest(response);
            }

            return Ok(response);
        }

        // PUT api/<CharacterController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterGetDto>>> Put(int id, [FromBody] CharacterUpdateDto character)
        {
            var response = await characterService.Update(id, character);

            if (response.Status == false)
            {
                if (response.StatusCode == (int)HttpStatusCode.NotFound) return NotFound(response);
                if (response.StatusCode == (int)HttpStatusCode.BadRequest) return BadRequest(response);
            }

            return Ok(response);
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await characterService.Delete(id);
        }
    }
}
