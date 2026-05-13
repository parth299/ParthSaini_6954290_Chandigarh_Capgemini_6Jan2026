using AutoMapper;
using BookStore.Application.DTOs.Order;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;

    private readonly IBookRepository _bookRepo;

    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepo,
        IBookRepository bookRepo,
        IMapper mapper)
    {
        _orderRepo = orderRepo;
        _bookRepo = bookRepo;
        _mapper = mapper;
    }

    public async Task<OrderResponseDto>
        PlaceOrderAsync(
            OrderCreateDto dto)
    {
        decimal total = 0;

        var orderItems =
            new List<OrderItem>();

        foreach (var item in dto.Items)
        {
            var book =
                await _bookRepo
                .GetByIdAsync(item.BookId);

            if (book.Stock < item.Qty)
                throw new Exception(
                    "Insufficient stock");

            book.Stock -= item.Qty;

            total +=
                book.Price * item.Qty;

            orderItems.Add(
                new OrderItem
                {
                    BookId = book.Id,
                    Qty = item.Qty,
                    Price = book.Price
                });
        }

        var order = new Order
        {
            UserId = dto.UserId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = total,
            Status = "Pending",
            OrderItems = orderItems
        };

        await _orderRepo.AddAsync(order);

        await _orderRepo.SaveAsync();

        return _mapper
            .Map<OrderResponseDto>(order);
    }

    public async Task<OrderResponseDto>
        GetOrderAsync(int orderId)
    {
        var order =
            await _orderRepo
            .GetOrderWithItemsAsync(orderId);

        return _mapper
            .Map<OrderResponseDto>(order);
    }
}