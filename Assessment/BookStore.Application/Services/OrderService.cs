using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using AutoMapper;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository, IMapper mapper, IEmailService emailService)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<OrderResponseDto?> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderWithItemsAsync(orderId);
        return order == null ? null : _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetUserOrdersAsync(int userId)
    {
        var orders = await _orderRepository.GetUserOrdersAsync(userId);
        return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }

    public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto dto)
    {
        decimal totalAmount = 0;
        var orderItems = new List<OrderItem>();

        foreach (var item in dto.OrderItems)
        {
            var book = await _bookRepository.GetByIdAsync(item.BookId);
            if (book == null)
                throw new Exception($"Book with ID {item.BookId} not found");

            if (book.Stock < item.Qty)
                throw new Exception($"Insufficient stock for book: {book.Title}");

            orderItems.Add(new OrderItem
            {
                BookId = item.BookId,
                Qty = item.Qty,
                Price = book.Price
            });

            totalAmount += book.Price * item.Qty;
            book.Stock -= item.Qty;
            await _bookRepository.UpdateAsync(book);
        }

        var order = new Order
        {
            UserId = dto.UserId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            Status = "Pending",
            OrderItems = orderItems
        };

        var createdOrder = await _orderRepository.AddAsync(order);
        await _orderRepository.SaveAsync();

        return _mapper.Map<OrderResponseDto>(createdOrder);
    }

    public async Task UpdateOrderStatusAsync(OrderStatusUpdateDto dto)
    {
        var order = await _orderRepository.GetByIdAsync(dto.OrderId);
        if (order == null)
            throw new Exception("Order not found");

        order.Status = dto.Status;
        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveAsync();
    }
}
