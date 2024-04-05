using Microsoft.AspNetCore.Mvc;
using WebApiCore8Sample.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore8Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private static Character character = new Character();
        private static List<Character> characters = new List<Character>();

        // GET: api/<CharacterController>
        [HttpGet]
        public ActionResult<Character> Get()
        {
            return Ok(character);
        }

        // GET api/<CharacterController>/5
        [HttpGet("{id}")]
        public ActionResult<Character> Get(int id)
        {
            var c =  characters.FirstOrDefault(item => item.Id == id);

            if (c == null) return NotFound();

            return Ok(character);
        }

        // POST api/<CharacterController>
        [HttpPost]
        public ActionResult<List<Character>> Post([FromBody] Character character)
        {
            characters.Add(character);
            return Ok(characters);
        }

        // PUT api/<CharacterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CharacterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
