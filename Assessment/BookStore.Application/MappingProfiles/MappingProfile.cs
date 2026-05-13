using AutoMapper;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role!.RoleName));

        CreateMap<UserProfile, UserProfileDto>();

        // Book mappings
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author!.Name))
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher!.Name));

        CreateMap<BookCreateDto, Book>();
        CreateMap<BookUpdateDto, Book>();

        // Category mappings
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateDto, Category>();

        // Author mappings
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorCreateDto, Author>();

        // Publisher mappings
        CreateMap<Publisher, PublisherDto>();
        CreateMap<PublisherCreateDto, Publisher>();

        // Order mappings
        CreateMap<Order, OrderResponseDto>();

        CreateMap<OrderItem, OrderItemResponseDto>()
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book!.Title));

        // Review mappings
        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewCreateDto, Review>();

        // Wishlist mappings
        CreateMap<Wishlist, WishlistDto>()
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book!.Title));
    }
}
