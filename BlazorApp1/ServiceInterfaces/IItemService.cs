using BlazorApp1.Entities.Request;
using BlazorApp1.Helpers;
using BlazorApp1.ServiceInterfaces.Internal;

namespace BlazorApp1.ServiceInterfaces
{
    public interface IItemService : IGenericService<ItemRequest>
    {
        Task<ApiResponse> GetItembyCategoryId(int? cid);
    }
}
