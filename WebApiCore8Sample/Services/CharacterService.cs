using Microsoft.AspNetCore.Http.HttpResults;
using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Services
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character()
            {
                Id = 1
            },
            new Character()
            {
                Id = 2
            }
        };

        public async Task<ServiceResponse<List<Character>>> Get()
        {
            var response = new ServiceResponse<List<Character>>();

            response.Data = characters;
            return response;
        }

        public async Task<ServiceResponse<Character>> Get(int id)
        {
            var response = new ServiceResponse<Character>();

            var character = characters.FirstOrDefault(item => item.Id == id);

            if (character == null)
            {
                response.Data = null;
                response.Status = false;
                response.Detail = "Not found";

                return response;
            }

            response.Data = character;
            return response;
        }

        public async Task<ServiceResponse<Character>> Add(Character character)
        {
            var response = new ServiceResponse<Character>();

            if (character == null)
            {
                response.Data = null;
                response.Status = false;
                response.Detail = "Not found";

                return response;
            }

            var c = characters.FirstOrDefault(item => item.Name == character.Name);

            if (c == null)
            {
                characters.Add(c);
                response.Data = character;
            }

            return response;
        }

        public async Task<ServiceResponse<Character>> Update(int id, Character character)
        {
            var response = new ServiceResponse<Character>();

            if (character == null)
            {
                response.Data = null;
                response.Status = false;
                response.Detail = "Not found";

                return response;
            }

            var c = characters.FirstOrDefault(item => item.Id == id);

            if (c == null)
            {
                response.Data = null;
                response.Status = false;
                response.Detail = "Not found";

                return response;
            }

            c.Name = character.Name;
            c.HintPoints = character.HintPoints;
            c.Strength = character.Strength;
            c.Defence = character.Defence;
            c.Intelligence = character.Intelligence;
            c.Class = character.Class;

            response.Data = c;
            return response;
        }

        public async Task Delete(int id)
        {
            var c = characters.FirstOrDefault(item => item.Id == id);

            if (c == null) return;

            characters.Remove(c);
        }

        public async Task Delete(Character character)
        {
            if (character == null) return;

            var c = characters.FirstOrDefault(item => item.Id == character.Id);

            if (c == null) return;

            characters.Remove(c);
        }
    }
}
