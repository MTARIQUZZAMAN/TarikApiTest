﻿using BlazorApp1.Entities.Request;
using BlazorApp1.Helpers;
using BlazorApp1.ServiceInterfaces;
using BlazorApp1.ServiceInterfaces.Internal;
using BlazorApp1.Services.Internal;

namespace BlazorApp1.Services
{
    public class ItemService : GenericService<ItemRequest>, IItemService
    {
        private readonly IHttpClientService _httpClientService;
        private const string _controllerName = "items";
        public ItemService(IHttpClientService httpClientService) : base(httpClientService, _controllerName)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse> GetItembyCategoryId(int? cid)
        {
            return await _httpClientService.Get($"{_controllerName}/GetByCategoryId", false, cid);
        }
    }
}
