using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using AutoMapper;

namespace BookStore.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public ReviewService(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReviewDto?> GetReviewByIdAsync(int reviewId)
    {
        var review = await _repository.GetByIdAsync(reviewId);
        return review == null ? null : _mapper.Map<ReviewDto>(review);
    }

    public async Task<IEnumerable<ReviewDto>> GetBookReviewsAsync(int bookId)
    {
        var reviews = await _repository.GetBookReviewsAsync(bookId);
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto> CreateReviewAsync(int userId, ReviewCreateDto dto)
    {
        var review = _mapper.Map<BookStore.Domain.Entities.Review>(dto);
        review.UserId = userId;
        
        var createdReview = await _repository.AddAsync(review);
        await _repository.SaveAsync();
        
        return _mapper.Map<ReviewDto>(createdReview);
    }

    public async Task DeleteReviewAsync(int reviewId)
    {
        await _repository.DeleteAsync(reviewId);
        await _repository.SaveAsync();
    }
}
