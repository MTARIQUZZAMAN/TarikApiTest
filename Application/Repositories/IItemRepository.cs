using Domain.Models;

namespace Application.Repositories
{
    public interface IItemRepository
    {
        Task<List<ItemModel>> GetItems();
        Task<ItemModel?> GetItem(int id);
        Task<int> InsertItem(ItemModel itemModel);
        Task<int> UpdateItem(ItemModel itemModel);
        Task<int> DeleteItem(int id);
    }
}
