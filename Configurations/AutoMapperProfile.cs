using AutoMapper;
using NhaSachDaiThang_BE_API.Models.Dtos;
using NhaSachDaiThang_BE_API.Models.Entities;
namespace NhaSachDaiThang_BE_API.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>().ForMember(dest => dest.BookCoverTypeName, opt => opt.MapFrom(src => src.BookCoverType.Name))
                .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.Language.Name));
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierDto, Supplier>();
            CreateMap<SupplierBookDto, SupplierBook>();
            CreateMap<SupplierBook, SupplierBookDto>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.Book.MainImage))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.Book.MainImage))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title)); ;
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<BookCoverType, BookCoverTypeDto>();
            CreateMap<BookCoverTypeDto, BookCoverType>();
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDto, Language>();
            CreateMap<Role, RoleDto>();
            CreateMap<User, UserDTO>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<UserDTO, User>();
        }
    }
}
