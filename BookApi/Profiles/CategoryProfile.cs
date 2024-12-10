using AutoMapper;
using Library.BookEntities;

namespace BookApi.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<CategoryDto, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());
        }
    }
}
