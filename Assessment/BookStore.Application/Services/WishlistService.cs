using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using AutoMapper;

namespace BookStore.Application.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _repository;
    private readonly IMapper _mapper;

    public WishlistService(IWishlistRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WishlistDto>> GetUserWishlistAsync(int userId)
    {
        var wishlists = await _repository.GetUserWishlistAsync(userId);
        return _mapper.Map<IEnumerable<WishlistDto>>(wishlists);
    }

    public async Task AddToWishlistAsync(int userId, WishlistCreateDto dto)
    {
        await _repository.AddToWishlistAsync(userId, dto.BookId);
    }

    public async Task RemoveFromWishlistAsync(int userId, int bookId)
    {
        await _repository.RemoveFromWishlistAsync(userId, bookId);
    }
}
