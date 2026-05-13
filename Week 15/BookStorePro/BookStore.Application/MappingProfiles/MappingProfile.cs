using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.Application.DTOs.Book;
using BookStore.Application.DTOs.Order;

namespace BookStore.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book → BookDto

        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(
                    src => src.Category.Name))

            .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(
                    src => src.Author.Name))

            .ForMember(
                dest => dest.PublisherName,
                opt => opt.MapFrom(
                    src => src.Publisher.Name));

        // CreateDto → Book

        CreateMap<BookCreateDto, Book>();

        // UpdateDto → Book

        CreateMap<BookUpdateDto, Book>();


        // Order → ResponseDto

        CreateMap<Order, OrderResponseDto>()
            .ForMember(
                dest => dest.OrderId,
                opt => opt.MapFrom(src => src.Id));

        CreateMap<OrderItem,
                  OrderItemResponseDto>()
            .ForMember(
                dest => dest.BookTitle,
                opt => opt.MapFrom(
                    src => src.Book.Title));
    }
}