using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using WebApiCore8Sample.Dtos;
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

        private readonly IMapper mapper;

        public CharacterService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<CharacterGetDto>>> Get()
        {
            var response = new ServiceResponse<List<CharacterGetDto>>();

            response.Data = mapper.Map<List<CharacterGetDto>>(characters);
            return response;
        }

        public async Task<ServiceResponse<CharacterGetDto>> Get(int id)
        {
            var response = new ServiceResponse<CharacterGetDto>();

            var character = characters.FirstOrDefault(item => item.Id == id);

            if (character == null)
            {
                response.Data = null;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Detail = "Not found.";

                return response;
            }

            response.Data = mapper.Map<CharacterGetDto>(character);
            return response;
        }

        public async Task<ServiceResponse<CharacterGetDto>> Add(CharacterAddDto characterInput)
        {
            var response = new ServiceResponse<CharacterGetDto>();

            if (characterInput == null)
            {
                response.Data = null;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Detail = "Not found.";

                return response;
            }

            if (characterInput.Name == "")
            {
                response.Data = null;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Detail = "Bad request. Name can not be empty.";

                return response;
            }

            var character = characters.FirstOrDefault(item => item.Name == characterInput.Name);

            if (character != null)
            {
                response.Data = null;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Detail = "Bad request. Item already exists.";

                return response;
            }

            var c = mapper.Map<Character>(characterInput);
            c.Id = characters.Max(item => item.Id) + 1;

            characters.Add(c);
            response.Data = mapper.Map<CharacterGetDto>(c);
            return response;
        }

        public async Task<ServiceResponse<CharacterGetDto>> Update(int id, CharacterUpdateDto characterInput)
        {
            var response = new ServiceResponse<CharacterGetDto>();

            var character = characters.FirstOrDefault(item => item.Id == id);

            if (character == null)
            {
                response.Data = null;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Detail = "Not found.";

                return response;
            }

            if (characterInput == null)
            {
                response.Data = null;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Detail = "Not found.";

                return response;
            }

            if (characterInput.Name == "")
            {
                response.Data = null;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Detail = "Bad request. Name can not be empty.";

                return response;
            }

            response.Data = mapper.Map<CharacterGetDto>(mapper.Map(characterInput, character));
            return response;
        }

        public async Task Delete(int id)
        {
            var c = characters.FirstOrDefault(item => item.Id == id);

            if (c == null) return;

            characters.Remove(c);
        }
    }
}
