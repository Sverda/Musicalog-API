using Musicalog.Application.Common.Interfaces;
using Musicalog.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Musicalog.Infrastructure.Interfaces;

namespace Musicalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDapper, Persistence.Dapper>();
            services.AddTransient<IAlbumRepository, AlbumRepository>();

            return services;
        }
    }
}
