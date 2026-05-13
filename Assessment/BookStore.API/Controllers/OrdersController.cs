using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IEmailService _emailService;

    public OrdersController(IOrderService orderService, IEmailService emailService)
    {
        _orderService = orderService;
        _emailService = emailService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrder(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
            return NotFound(new { message = "Order not found" });

        return Ok(order);
    }

    [HttpGet("user/myorders")]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetMyOrders()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        var orders = await _orderService.GetUserOrdersAsync(userId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder(OrderCreateDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        dto.UserId = userId;

        var order = await _orderService.CreateOrderAsync(dto);
        
        // Send confirmation email
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value ?? "";
        await _emailService.SendOrderConfirmationAsync(userEmail, order.OrderId);

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
    }

    [HttpPut("status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateOrderStatus(OrderStatusUpdateDto dto)
    {
        await _orderService.UpdateOrderStatusAsync(dto);
        return NoContent();
    }
}
