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

        public async Task<List<Character>> Get()
        {
            return characters;
        }

        public async Task<Character> Get(int id)
        {
            var c = characters.FirstOrDefault(item => item.Id == id);

            if (c == null)
            {
                throw new Exception(nameof(NotFound));
            }

            return c;
        }

        public async Task<Character> Add(Character character)
        {
            characters.Add(character);
            return character;
        }

        public async Task<Character> Update(int id, Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(Character));
            }

            var c = characters.FirstOrDefault(item => item.Id == id);

            if (c == null)
            {
                throw new Exception(nameof(NotFound));
            }

            c.Name = character.Name;
            c.HintPoints = character.HintPoints;
            c.Strength = character.Strength;
            c.Defence = character.Defence;
            c.Intelligence = character.Intelligence;
            c.Class = character.Class;

            return c;
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
