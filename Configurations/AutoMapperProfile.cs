using AutoMapper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
namespace NhaSachDaiThang_BE_API.Configurations
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile() {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<User, UserDTO>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
        }
    }
}
