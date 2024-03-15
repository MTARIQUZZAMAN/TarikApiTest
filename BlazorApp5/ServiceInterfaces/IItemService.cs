using BlazorApp5.DTOs;
using BlazorApp5.Helpers;

namespace BlazorApp5.ServiceInterfaces
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
