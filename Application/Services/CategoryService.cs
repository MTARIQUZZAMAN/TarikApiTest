using Application.Entities.Requests;
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

        public async Task<List<CategoryRequest>?> GetAll()
        {
            var models = await _repository.GetAll();
            if (models == null || models.Count <= 0) return null;
            var vms = _mapper.Map<List<CategoryRequest>>(models);
            return vms;
        }

        public async Task<CategoryRequest?> GetById(int id)
        {
            var model = await _repository.GetById(id);
            if (model == null) return default;
            var vms = _mapper.Map<CategoryRequest>(model);
            return vms;
        }

        public async Task<CategoryRequest?> Create(CategoryRequest dto)
        {
            if (dto == null) return default;
            var model = _mapper.Map<CategoryModel>(dto);
            var rowsAffected = await _repository.Insert(model);
            if (rowsAffected <= 0) return default;
            var createdDto = _mapper.Map<CategoryRequest>(model);
            return createdDto;
        }



        public async Task<CategoryRequest?> Update(CategoryRequest dto)
        {
            if (dto == null) return default;
            var model = _mapper.Map<CategoryModel>(dto);
            var rowsAffected = await _repository.Update(model);
            if (rowsAffected <= 0) return default;
            var updatedDto = _mapper.Map<CategoryRequest>(model);
            return updatedDto;
        }

        public async Task<int> Delete(int id)
        {
            if (id <= 0) return -1;
            var rowsDeleted = await _repository.Delete(id);
            return rowsDeleted;
        }


    }
}
