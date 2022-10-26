using Microsoft.EntityFrameworkCore;

namespace StarWars.Infra.Data.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }
    }
}