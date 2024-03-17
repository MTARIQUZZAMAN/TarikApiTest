using BlazorApp1.DTOs;
using BlazorApp1.Helpers;
using BlazorApp1.ServiceInterfaces.Internal;

namespace BlazorApp1.ServiceInterfaces
{
    public interface IItemService : IGenericService<ItemDTO>
    {
        Task<ApiResponse> GetItembyCategoryId(int? cid);
    }
}
