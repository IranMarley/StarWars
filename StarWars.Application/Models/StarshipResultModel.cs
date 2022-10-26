using StarWars.Domain.Entities;

namespace StarWars.Application.Models
{
    public class StarshipResultModel
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<StarshipEntity> results { get; set; }
    }
}
