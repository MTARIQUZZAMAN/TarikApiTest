using Application.Entities.Requests;
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

        public async Task<List<ItemRequest>?> GetAll()
        {
            var models = await _repository.GetAll();
            if (models == null || models.Count <= 0) return null;
            var vms = _mapper.Map<List<ItemRequest>>(models);
            return vms;
        }

        public async Task<ItemRequest?> GetById(int id)
        {
            var model = await _repository.GetById(id);
            if (model == null) return default;
            var vms = _mapper.Map<ItemRequest>(model);
            return vms;
        }

        public async Task<ItemRequest?> Create(ItemRequest dto)
        {
            if (dto == null) return default;
            var model = _mapper.Map<ItemModel>(dto);
            model.DateCreated = DateTime.UtcNow;

            var newId = await _repository.Insert(model);
            if (newId <= 0) return default;
            model.Id = newId;
            var createdDto = _mapper.Map<ItemRequest>(model);
            return createdDto;
        }


        public async Task<ItemRequest?> Update(ItemRequest dto)
        {
            if (dto == null) return default;
            var model = _mapper.Map<ItemModel>(dto);
            model.DateUpdated = DateTime.UtcNow;

            var rowsAffected = await _repository.Update(model);
            if (rowsAffected <= 0) return default;
            var updatedDto = _mapper.Map<ItemRequest>(model);
            return updatedDto;
        }

        public async Task<int> Delete(int id)
        {
            if (id <= 0) return -1;
            var rowsDeleted = await _repository.Delete(id);
            return rowsDeleted;
        }

        public async Task<List<ItemRequest>> GetByCategegoryId(int? cid)
        {
            if (cid <= 0) return null;
            var models = await _repository.GetByCategoryId(cid);
            if (models == null) return null;
            var vms = _mapper.Map<List<ItemRequest>>(models);
            return vms;
        }




    }
}
