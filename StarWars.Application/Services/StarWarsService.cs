using AutoMapper;
using Newtonsoft.Json;
using StarWars.Application.Interfaces;
using StarWars.Application.Models;
using StarWars.Domain.Entities;
using StarWars.Infra.CrossCutting.Support;

namespace StarWars.Application.Services
{
    public class StarWarsService : IStarWarsService
    {
        private readonly string URL = "https://swapi.dev/api/starships/"; 
        private readonly IMapper _mapper;

        public StarWarsService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<string> GetManufacturer()
        {
            return String.Join(',', this.GetStarShips(new StarWarsFilterModel())
                .Select(s => s.manufacturer))
                .Split(',')
                .Select(s => s.Trim())
                .Distinct()
                .ToList();
        }

        public IEnumerable<StarshipModel> GetStarShips(StarWarsFilterModel filter)
        {
            var starships = new List<StarshipEntity>();

            int pageNum = 0;

            while (true)
            {
                var page = JsonConvert.DeserializeObject<StarshipResultModel>(UtilsRequest.Get($"{URL}?page={++pageNum}"));

                starships.AddRange(page.results);

                if (page.next == null)
                    break;
            }

            if (!string.IsNullOrEmpty(filter.Manufacturer))
                starships = starships.Where(w => w.manufacturer.Contains(filter.Manufacturer)).ToList();

            return _mapper.Map<IEnumerable<StarshipModel>>(starships);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
