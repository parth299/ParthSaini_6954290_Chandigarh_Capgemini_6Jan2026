using Microsoft.AspNetCore.Mvc;
using BookStore.Application.Interfaces;
using BookStore.Application.DTOs.Order;

namespace BookStore.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(
        IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult>
        PlaceOrder(
            OrderCreateDto dto)
    {
        var order =
            await _orderService
            .PlaceOrderAsync(dto);

        return Ok(order);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>
        GetOrder(int id)
    {
        var order =
            await _orderService
            .GetOrderAsync(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }
}