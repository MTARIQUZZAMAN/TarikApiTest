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
        public static IServiceCollection AddInfractructure(this IServiceCollection services)
        {
            services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            services.AddSingleton<IItemRepository, ItemRepository>();

            return services;
        }

    }
}
