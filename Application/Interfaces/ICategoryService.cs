using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICategoryService
    {

        Task<List<CategoryDTO>> GetItems();
        Task<CategoryDTO> GetItem(int id);
        Task<CategoryDTO> Create(CategoryDTO dto);
        Task<CategoryDTO> Update(CategoryDTO dto);
        Task<int> Delete(int id);

    }
}
