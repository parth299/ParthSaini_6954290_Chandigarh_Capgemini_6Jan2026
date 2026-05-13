using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using AutoMapper;

namespace BookStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _repository.GetByIdAsync(categoryId);
        return category == null ? null : _mapper.Map<CategoryDto>(category);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<BookStore.Domain.Entities.Category>(dto);
        var createdCategory = await _repository.AddAsync(category);
        await _repository.SaveAsync();
        return _mapper.Map<CategoryDto>(createdCategory);
    }

    public async Task UpdateCategoryAsync(int categoryId, CategoryCreateDto dto)
    {
        var category = await _repository.GetByIdAsync(categoryId);
        if (category == null)
            throw new Exception("Category not found");

        category.Name = dto.Name;
        await _repository.UpdateAsync(category);
        await _repository.SaveAsync();
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        await _repository.DeleteAsync(categoryId);
        await _repository.SaveAsync();
    }
}

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int authorId)
    {
        var author = await _repository.GetByIdAsync(authorId);
        return author == null ? null : _mapper.Map<AuthorDto>(author);
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
    {
        var authors = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    public async Task<AuthorDto> CreateAuthorAsync(AuthorCreateDto dto)
    {
        var author = _mapper.Map<BookStore.Domain.Entities.Author>(dto);
        var createdAuthor = await _repository.AddAsync(author);
        await _repository.SaveAsync();
        return _mapper.Map<AuthorDto>(createdAuthor);
    }

    public async Task UpdateAuthorAsync(int authorId, AuthorCreateDto dto)
    {
        var author = await _repository.GetByIdAsync(authorId);
        if (author == null)
            throw new Exception("Author not found");

        author.Name = dto.Name;
        await _repository.UpdateAsync(author);
        await _repository.SaveAsync();
    }

    public async Task DeleteAuthorAsync(int authorId)
    {
        await _repository.DeleteAsync(authorId);
        await _repository.SaveAsync();
    }
}

public class PublisherService : IPublisherService
{
    private readonly IPublisherRepository _repository;
    private readonly IMapper _mapper;

    public PublisherService(IPublisherRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PublisherDto?> GetPublisherByIdAsync(int publisherId)
    {
        var publisher = await _repository.GetByIdAsync(publisherId);
        return publisher == null ? null : _mapper.Map<PublisherDto>(publisher);
    }

    public async Task<IEnumerable<PublisherDto>> GetAllPublishersAsync()
    {
        var publishers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<PublisherDto>>(publishers);
    }

    public async Task<PublisherDto> CreatePublisherAsync(PublisherCreateDto dto)
    {
        var publisher = _mapper.Map<BookStore.Domain.Entities.Publisher>(dto);
        var createdPublisher = await _repository.AddAsync(publisher);
        await _repository.SaveAsync();
        return _mapper.Map<PublisherDto>(createdPublisher);
    }

    public async Task UpdatePublisherAsync(int publisherId, PublisherCreateDto dto)
    {
        var publisher = await _repository.GetByIdAsync(publisherId);
        if (publisher == null)
            throw new Exception("Publisher not found");

        publisher.Name = dto.Name;
        await _repository.UpdateAsync(publisher);
        await _repository.SaveAsync();
    }

    public async Task DeletePublisherAsync(int publisherId)
    {
        await _repository.DeleteAsync(publisherId);
        await _repository.SaveAsync();
    }
}
