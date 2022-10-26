using AutoMapper;
using StarWars.Application.Models;
using StarWars.Domain.Entities;

namespace StarWars.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<StarshipModel, StarshipEntity>();
        }
    }
}
