

using AutoMapper;
using Repositories.Categories;
using Repositories.Products;
using Services.Categories.Create;
using Services.Categories.Dto;
using Services.Categories.Update;
using Services.Products.Request;
using Services.Products.Update;

namespace Services.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() {

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();

            CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.name.ToLowerInvariant()));

            CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.name.ToLowerInvariant()));

        }
    }
}
