using Microsoft.Extensions.DependencyInjection;
using Mongo.Data.Interfaces;
using Mongo.Data.Repositories;
using Mongo.Service.Interfaces;
using Mongo.Service.Services;

namespace Mongo.Api.IoC
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRestauranteService, RestauranteService>();
            services.AddScoped<IRestauranteRepository, RestauranteRepository>();

            return services;
        }
    }
}
