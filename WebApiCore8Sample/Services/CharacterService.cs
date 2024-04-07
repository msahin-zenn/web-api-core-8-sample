using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApiCore8Sample.Data;
using WebApiCore8Sample.Dtos;
using WebApiCore8Sample.Models;

namespace WebApiCore8Sample.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ServiceResponse<List<CharacterGetDto>>> Get()
        {
            var response = new ServiceResponse<List<CharacterGetDto>>();

            var characters = await context.Characters.ToListAsync();
            response.Data = mapper.Map<List<CharacterGetDto>>(characters);
            return response;
        }

        public async Task<ServiceResponse<CharacterGetDto>> Get(int id)
        {
            var response = new ServiceResponse<CharacterGetDto>();

            var character = await context.Characters.FirstOrDefaultAsync(item => item.Id == id);

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

            var character = await context.Characters.FirstOrDefaultAsync(item => item.Name == characterInput.Name);

            if (character != null)
            {
                response.Data = null;
                response.Status = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Detail = "Bad request. Item already exists.";

                return response;
            }

            var c = mapper.Map<Character>(characterInput);

            context.Characters.Add(c);
            await context.SaveChangesAsync();

            response.Data = mapper.Map<CharacterGetDto>(c);
            return response;
        }

        public async Task<ServiceResponse<CharacterGetDto>> Update(int id, CharacterUpdateDto characterInput)
        {
            var response = new ServiceResponse<CharacterGetDto>();

            var character = await context.Characters.FirstOrDefaultAsync(item => item.Id == id);

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

            var updatedCharacter = mapper.Map(characterInput, character);
            await context.SaveChangesAsync();

            response.Data = mapper.Map<CharacterGetDto>(updatedCharacter);
            return response;
        }

        public async Task Delete(int id)
        {
            var c = await context.Characters.FirstOrDefaultAsync(item => item.Id == id);

            if (c == null) return;

            context.Characters.Remove(c);
            await context.SaveChangesAsync();
        }
    }
}
