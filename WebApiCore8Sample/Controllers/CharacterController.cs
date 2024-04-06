using Microsoft.AspNetCore.Mvc;
using WebApiCore8Sample.Models;
using WebApiCore8Sample.Services;

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
        public async Task<ActionResult<ServiceResponse<Character>>> Get()
        {
            try
            {
                return Ok(await characterService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CharacterController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> Get(int id)
        {
            try
            {
                return Ok(await characterService.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CharacterController>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Character>>> Post([FromBody] Character character)
        {
            try
            {
                return Ok(await characterService.Add(character));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CharacterController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Character character)
        {
            try
            {
                await characterService.Update(id, character);
            }
            catch (Exception ex)
            {
            }
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await characterService.Delete(id);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
