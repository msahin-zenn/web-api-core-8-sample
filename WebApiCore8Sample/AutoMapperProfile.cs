using AutoMapper;
using WebApiCore8Sample.Dtos;
using WebApiCore8Sample.Models;

namespace WebApiCore8Sample
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterGetDto>();

            CreateMap<CharacterAddDto, Character>();
            CreateMap<CharacterAddDto, CharacterGetDto>();

            CreateMap<CharacterUpdateDto, Character>();
            CreateMap<CharacterUpdateDto, CharacterGetDto>();
        }
    }
}
