using AutoMapper;
using BookStore.Application.DTOs.Book;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepo;

    private readonly IMapper _mapper;

    public BookService(
        IBookRepository bookRepo,
        IMapper mapper)
    {
        _bookRepo = bookRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>>
        GetAllBooksAsync()
    {
        var books =
            await _bookRepo
            .GetBooksWithCategoryAsync();

        return _mapper
            .Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto>
        GetBookByIdAsync(int id)
    {
        var book =
            await _bookRepo
            .GetByIdAsync(id);

        return _mapper
            .Map<BookDto>(book);
    }

    public async Task CreateBookAsync(
        BookCreateDto dto)
    {
        var book =
            _mapper.Map<Book>(dto);

        await _bookRepo.AddAsync(book);

        await _bookRepo.SaveAsync();
    }

    public async Task UpdateBookAsync(
        BookUpdateDto dto)
    {
        var book =
            await _bookRepo
            .GetByIdAsync(dto.Id);

        _mapper.Map(dto, book);

        _bookRepo.Update(book);

        await _bookRepo.SaveAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book =
            await _bookRepo.GetByIdAsync(id);

        _bookRepo.Delete(book);

        await _bookRepo.SaveAsync();
    }
}