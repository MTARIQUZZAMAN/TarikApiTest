using Application.DTOs;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Domain.Models;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        protected readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>?> GetItems()
        {
            var models = await _repository.GetItems();
            if (models == null || models.Count <= 0) return null;
            var vms = _mapper.Map<List<CategoryDTO>>(models);
            return vms;
        }

        public async Task<CategoryDTO?> GetItem(int id)
        {
            var model = await _repository.GetItem(id);
            if (model == null) return default;
            var vms = _mapper.Map<CategoryDTO>(model);
            return vms;
        }

        public async Task<CategoryDTO?> Create(CategoryDTO dto)
        {
            if (dto == null) return default;
            var model = _mapper.Map<CategoryModel>(dto);
            var rowsAffected = await _repository.InsertItem(model);
            if (rowsAffected <= 0) return default;
            var createdDto = _mapper.Map<CategoryDTO>(model);
            return createdDto;
        }

        public async Task<CategoryDTO?> Update(CategoryDTO dto)
        {
            if (dto == null) return default;
            var model = _mapper.Map<CategoryModel>(dto);
            var rowsAffected = await _repository.UpdateItem(model);
            if (rowsAffected <= 0) return default;
            var updatedDto = _mapper.Map<CategoryDTO>(model);
            return updatedDto;
        }

        public async Task<int> Delete(int id)
        {
            if (id <= 0) return -1;
            var rowsDeleted = await _repository.DeleteItem(id);
            return rowsDeleted;
        }
    }
}
