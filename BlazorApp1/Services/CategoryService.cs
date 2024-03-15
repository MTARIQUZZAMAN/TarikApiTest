using BlazorApp1.DTOs;
using BlazorApp1.Helpers;
using BlazorApp1.ServiceInterfaces;
using BlazorApp1.ServiceInterfaces.Internal;

namespace BlazorApp1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientService _httpClientService;
        public CategoryService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ApiResponse> Get()
        {

            return await _httpClientService.Get("categories/get", false);
        }

        public async Task<ApiResponse> Get(int id)
        {
            return await _httpClientService.Get("categories/get", false, id);
        }

        public async Task<ApiResponse> Create(CategoryDTO modelDto)
        {
            return await _httpClientService.Post("categories/create", false, modelDto);
        }

        public async Task<ApiResponse> Update(int id, CategoryDTO modelDto)
        {
            return await _httpClientService.Put("categories/update", false, id, modelDto);
        }

        public async Task<ApiResponse> Delete(int id)
        {
            return await _httpClientService.Delete("categories/delete", false, id);
        }



    }
}
