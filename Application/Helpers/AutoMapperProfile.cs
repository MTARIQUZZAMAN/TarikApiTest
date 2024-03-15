using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ItemModel, ItemDTO>();
            CreateMap<ItemDTO, ItemModel>();


            CreateMap<CategoryModel, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryModel>();
        }
    }
}
