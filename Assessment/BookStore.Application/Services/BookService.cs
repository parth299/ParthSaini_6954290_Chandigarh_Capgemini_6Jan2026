using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using AutoMapper;

namespace BookStore.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookDto?> GetBookByIdAsync(int bookId)
    {
        var book = await _repository.GetByIdAsync(bookId);
        return book == null ? null : _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<IEnumerable<BookDto>> GetBooksByCategoryAsync(int categoryId)
    {
        var books = await _repository.GetByCategoryAsync(categoryId);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<IEnumerable<BookDto>> SearchBooksAsync(string keyword)
    {
        var books = await _repository.SearchBooksAsync(keyword);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto> CreateBookAsync(BookCreateDto dto)
    {
        var book = _mapper.Map<BookStore.Domain.Entities.Book>(dto);
        var createdBook = await _repository.AddAsync(book);
        await _repository.SaveAsync();
        return _mapper.Map<BookDto>(createdBook);
    }

    public async Task UpdateBookAsync(BookUpdateDto dto)
    {
        var book = _mapper.Map<BookStore.Domain.Entities.Book>(dto);
        await _repository.UpdateAsync(book);
        await _repository.SaveAsync();
    }

    public async Task DeleteBookAsync(int bookId)
    {
        await _repository.DeleteAsync(bookId);
        await _repository.SaveAsync();
    }
}
