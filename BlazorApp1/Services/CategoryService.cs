using BlazorApp1.DTOs;
using BlazorApp1.ServiceInterfaces;
using BlazorApp1.ServiceInterfaces.Internal;
using BlazorApp1.Services.Internal;

namespace BlazorApp1.Services
{
    public class CategoryService : GenericService<CategoryDTO>, ICategoryService
    {
        private readonly IHttpClientService _httpClientService;
        private const string _controllerName = "categories";
        public CategoryService(IHttpClientService httpClientService) : base(httpClientService, _controllerName)
        {
            _httpClientService = httpClientService;
        }



    }
}
