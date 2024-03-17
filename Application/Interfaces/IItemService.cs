using Application.Entities.Requests;

namespace Application.Interfaces
{
    public interface IItemService : IGenericService<ItemRequest>
    {
        Task<List<ItemRequest>> GetByCategegoryId(int? cid);

    }
}
