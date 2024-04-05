using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> Get();
        Task<Character> Get(int id);

        Task<Character> Add(Character character);
        Task<Character> Update(int id, Character character);

        Task Delete(int id);
        Task Delete(Character character);
    }
}
