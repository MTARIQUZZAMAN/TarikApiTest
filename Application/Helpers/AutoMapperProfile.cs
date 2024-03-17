using Application.Entities.Requests;
using AutoMapper;
using Domain.Models;

namespace Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<ItemRequest, ItemModel>();
            CreateMap<ItemModel, ItemRequest>();

            CreateMap<CategoryRequest, CategoryModel>();
            CreateMap<CategoryModel, CategoryRequest>();

        }
    }
}
