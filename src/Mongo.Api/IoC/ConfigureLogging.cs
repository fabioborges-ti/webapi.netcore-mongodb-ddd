using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mongo.Api.Controllers;

namespace Mongo.Api.IoC
{
    public static class ConfigureLogging
    {
        public static IServiceCollection ConfigureLoggingCollection(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<RestaurantesController>>());

            return services;
        }
    }
}
