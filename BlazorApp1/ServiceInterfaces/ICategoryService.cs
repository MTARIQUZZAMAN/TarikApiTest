using BlazorApp1.DTOs;
using BlazorApp1.Helpers;

namespace BlazorApp1.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int Id);
        Task<ApiResponse> Create(CategoryDTO modelDto);
        Task<ApiResponse> Update(int id, CategoryDTO modelDto);
        Task<ApiResponse> Delete(int id);
    }
}
