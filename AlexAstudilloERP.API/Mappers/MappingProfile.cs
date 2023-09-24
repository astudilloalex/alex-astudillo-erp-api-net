using AlexAstudilloERP.API.DTOs;
using AlexAstudilloERP.Domain.Entities.Public;
using AutoMapper;

namespace AlexAstudilloERP.API.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CompanyDTO, Company>();
        CreateMap<PersonDTO, Person>();
    }
}
