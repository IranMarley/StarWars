using StarWars.Application.Models;
using StarWars.Domain.Entities;

namespace StarWars.Application.Interfaces
{
    public interface IStarWarsService : IDisposable
    {
        IEnumerable<string> GetManufacturer();
        IEnumerable<StarshipModel> GetStarShips(StarWarsFilterModel filter);
    }
}
