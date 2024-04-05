using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Services
{
    public interface ICharacterService
    {
        List<Character> Get();
        Character Get(int id);

        Character Add(Character character);
        Character Update(int id, Character character);

        void Delete(int id);
        void Delete(Character character);
    }
}
