using Domain.Models;

namespace Application.Repositories
{
    public interface IItemRepository : IGenericRepository<ItemModel>
    {
        Task<List<ItemModel>> GetByCategoryId(int? cid);
    }
}
