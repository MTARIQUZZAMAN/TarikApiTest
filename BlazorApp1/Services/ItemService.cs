using BlazorApp1.DTOs;
using BlazorApp1.ServiceInterfaces;
using BlazorApp1.ServiceInterfaces.Internal;
using BlazorApp1.Services.Internal;

namespace BlazorApp1.Services
{
    public class ItemService : GenericService<ItemDTO>, IItemService
    {
        private readonly IHttpClientService _httpClientService;
        private const string _controllerName = "items";
        public ItemService(IHttpClientService httpClientService) : base(httpClientService, _controllerName)
        {
            _httpClientService = httpClientService;
        }




    }
}
