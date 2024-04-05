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

        public List<Character> Get()
        {
            return characters;
        }

        public Character Get(int id)
        {
            var c = characters.FirstOrDefault(item => item.Id == id);

            if (c == null)
            {
                throw new Exception(nameof(NotFound));
            }

            return c;
        }

        public Character Add(Character character)
        {
            characters.Add(character);
            return character;
        }

        public Character Update(int id, Character character)
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

        public void Delete(int id)
        {
            var c = characters.FirstOrDefault(item => item.Id == id);

            if (c == null) return;

            characters.Remove(c);
        }

        public void Delete(Character character)
        {
            if (character == null) return;

            var c = characters.FirstOrDefault(item => item.Id == character.Id);

            if (c == null) return;

            characters.Remove(c);
        }
    }
}
