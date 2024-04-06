using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> Get();
        Task<ServiceResponse<Character>> Get(int id);

        Task<ServiceResponse<Character>> Add(Character character);
        Task<ServiceResponse<Character>> Update(int id, Character character);

        Task Delete(int id);
        Task Delete(Character character);
    }
}
