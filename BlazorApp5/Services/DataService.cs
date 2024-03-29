﻿using BlazorApp5.ServiceInterfaces;

namespace BlazorApp5.Services
{
    public class DataService : IDataService
    {
        private readonly IHttpClientService _httpClientService;

        public IItemService StockItems { get; }

        public DataService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            StockItems = new ItemService(_httpClientService);
        }

    }
}
