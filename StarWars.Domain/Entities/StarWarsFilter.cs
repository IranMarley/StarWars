namespace StarWars.Domain.Entities
{
    public class StarWarsFilter
    {
        public string? Manufacturer { get; set; }

        public IQueryable<StarshipEntity> ApplyFilters(IQueryable<StarshipEntity> result)
        {
            if (!string.IsNullOrEmpty(Manufacturer))
                result = result.Where(w => w.manufacturer == this.Manufacturer);

            return result;
        }
    }
}
