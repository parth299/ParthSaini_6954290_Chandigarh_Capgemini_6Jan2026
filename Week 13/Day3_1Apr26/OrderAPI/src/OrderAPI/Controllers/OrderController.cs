using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;
using OrderAPI.Services;

namespace OrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Places a new order.
    /// </summary>
    /// <param name="order">The order to place.</param>
    /// <returns>201 Created if successful, 400 Bad Request if it fails.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PlaceOrder([FromBody] Order order)
    {
        var success = await _orderService.PlaceOrderAsync(order);

        if (!success)
            return BadRequest();

        return Created($"api/order/{order.Id}", order);
    }
}
