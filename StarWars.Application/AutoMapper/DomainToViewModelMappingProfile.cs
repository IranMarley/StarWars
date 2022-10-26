using AutoMapper;
using StarWars.Application.Models;
using StarWars.Domain.Entities;

namespace StarWars.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<StarshipEntity, StarshipModel>();
            CreateMap<StarWarsFilter, StarWarsFilterModel>();
        }
    }
}
