using Application.DTOs;

namespace Application.Interfaces
{
    public interface IItemService
    {

        Task<List<ItemDTO>> GetItems();
        Task<ItemDTO> GetItem(int id);
        Task<ItemDTO> Create(ItemDTO dto);
        Task<ItemDTO> Update(ItemDTO dto);
        Task<int> Delete(int id);

    }
}
