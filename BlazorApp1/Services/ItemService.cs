using BlazorApp1.DTOs;
using BlazorApp1.Helpers;
using BlazorApp1.ServiceInterfaces;

namespace BlazorApp1.Services
{
    public class ItemService : IItemService
    {
        private readonly IHttpClientService _httpClientService;
        public ItemService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse> Get()
        {
            return await _httpClientService.Get("items/get", false);
        }

        public async Task<ApiResponse> Get(int id)
        {
            return await _httpClientService.Get("items/get", false, id);
        }

        public async Task<ApiResponse> Create(ItemDTO modelDto)
        {
            return await _httpClientService.Post("items/create", false, modelDto);
        }

        public async Task<ApiResponse> Update(int id, ItemDTO modelDto)
        {
            return await _httpClientService.Put("items/update", false, id, modelDto);
        }

        public async Task<ApiResponse> Delete(int id)
        {
            return await _httpClientService.Delete("items/delete", false, id);
        }



    }
}
