using Application.Common.Interface;
using Application.Repositories;
using DataAccess.Internal;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfractructure(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("Default");

            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            services.AddSingleton<IItemRepository, ItemRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();

            return services;
        }

    }
}
