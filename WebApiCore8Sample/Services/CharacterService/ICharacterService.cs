using WebApiCore8Sample.Dtos;
using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<CharacterGetDto>>> Get();
        Task<ServiceResponse<CharacterGetDto>> Get(int id);
        Task<ServiceResponse<CharacterGetDto>> Add(CharacterAddDto character);
        Task<ServiceResponse<CharacterGetDto>> Update(int id, CharacterUpdateDto character);
        Task Delete(int id);
    }
}
