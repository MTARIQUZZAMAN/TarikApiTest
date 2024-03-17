using Application.DTOs;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Models;

namespace Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        protected readonly IMapper _mapper;

        public ItemService(IItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ItemDTO>?> GetItems()
        {
            var models = await _repository.GetItems();
            if (models == null || models.Count <= 0) return null;
            var vms = _mapper.Map<List<ItemDTO>>(models);
            return vms;
        }

        public async Task<ItemDTO?> GetItem(int id)
        {
            var model = await _repository.GetItem(id);
            if (model == null) return default;
            var vms = _mapper.Map<ItemDTO>(model);
            return vms;
        }

        public async Task<ItemDTO?> Create(ItemDTO dto)
        {
            if (dto == null) return default;
            var model = _mapper.Map<ItemModel>(dto);
            var newId = await _repository.InsertItem(model);
            if (newId <= 0) return default;
            model.Id = newId;
            var createdDto = _mapper.Map<ItemDTO>(model);
            return createdDto;
        }

        public async Task<ItemDTO?> Update(ItemDTO dto)
        {
            if (dto == null) return default;
            var model = _mapper.Map<ItemModel>(dto);
            var rowsAffected = await _repository.UpdateItem(model);
            if (rowsAffected <= 0) return default;
            var updatedDto = _mapper.Map<ItemDTO>(model);
            return updatedDto;
        }

        public async Task<int> Delete(int id)
        {
            if (id <= 0) return -1;
            var rowsDeleted = await _repository.DeleteItem(id);
            return rowsDeleted;
        }

        public async Task<List<ItemDTO>> GetByCategegoryId(int? cid)
        {
            if (cid <= 0) return null;
            var models = await _repository.GetByCategoryId(cid);
            if (models == null) return null;
            var vms = _mapper.Map<List<ItemDTO>>(models);
            return vms;
        }
    }
}
