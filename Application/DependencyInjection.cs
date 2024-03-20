using Application.Authentication;
using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {


            //add automapper
            var mapperConfig = new MapperConfiguration(o => o.AddProfile(new AutoMapperProfile()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //add services
            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
