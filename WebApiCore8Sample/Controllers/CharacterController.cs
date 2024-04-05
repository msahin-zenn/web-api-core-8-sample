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
        public ActionResult<Character> Get()
        {
            try
            {
                return Ok(characterService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CharacterController>/5
        [HttpGet("{id}")]
        public ActionResult<Character> Get(int id)
        {
            try
            {
                return Ok(characterService.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<CharacterController>
        [HttpPost]
        public ActionResult<Character> Post([FromBody] Character character)
        {
            try
            {
                return Ok(characterService.Add(character));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CharacterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Character character)
        {
            try
            {
                characterService.Update(id, character);
            }
            catch (Exception ex)
            {
            }
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                characterService.Delete(id);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
