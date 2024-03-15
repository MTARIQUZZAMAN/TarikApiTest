using BlazorApp1.ServiceInterfaces;
using BlazorApp1.ServiceInterfaces.Internal;

namespace BlazorApp1.Services.Internal
{
    public class DataService : IDataService
    {
        private readonly IHttpClientService _httpClientService;

        public IItemService StockItems { get; }
        public ICategoryService CategoryItems { get; }


        public DataService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;

            StockItems = new ItemService(_httpClientService);
            CategoryItems = new CategoryService(_httpClientService);
        }

    }
}
