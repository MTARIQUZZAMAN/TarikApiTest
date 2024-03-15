using Domain.Models;

namespace Application.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetItems();
        Task<CategoryModel?> GetItem(int id);
        Task<int> InsertItem(CategoryModel itemModel);
        Task<int> UpdateItem(CategoryModel itemModel);
        Task<int> DeleteItem(int id);
    }
}
