using BlazorApp1.DTOs;
using BlazorApp1.Helpers;

namespace BlazorApp1.ServiceInterfaces
{
    public interface IItemService
    {
        Task<ApiResponse> Get();
        Task<ApiResponse> Get(int Id);

        Task<ApiResponse> Create(ItemDTO modelDto);
        Task<ApiResponse> Update(int id, ItemDTO modelDto);
        Task<ApiResponse> Delete(int id);
    }
}
