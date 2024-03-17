using BlazorApp1.Entities.Request;
using BlazorApp1.ServiceInterfaces;
using BlazorApp1.ServiceInterfaces.Internal;
using BlazorApp1.Services.Internal;

namespace BlazorApp1.Services
{
    public class CategoryService : GenericService<CategoryRequest>, ICategoryService
    {
        private readonly IHttpClientService _httpClientService;
        private const string _controllerName = "categories";
        public CategoryService(IHttpClientService httpClientService) : base(httpClientService, _controllerName)
        {
            _httpClientService = httpClientService;
        }



    }
}
