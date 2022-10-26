using StarWars.Application.Interfaces;
using StarWars.Application.Services;
using StarWars.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace StarWars.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<IStarWarsService, StarWarsService>();

            // Infra - Data
            services.AddScoped<ApiContext>();

        }
    }
}
