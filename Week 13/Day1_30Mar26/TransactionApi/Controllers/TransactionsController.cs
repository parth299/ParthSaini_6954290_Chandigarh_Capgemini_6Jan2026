using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransactionApi.Services;
using TransactionApi.Helpers;
using TransactionApi.DTOs;

namespace TransactionApi.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionsController
        : ControllerBase
    {
        private readonly
            ITransactionService _service;

        public TransactionsController(
            ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
public async Task<IActionResult>
    GetTransactions(
        [FromQuery]
        QueryParameters queryParams)
{
    var userId =
        int.Parse(
            User.FindFirstValue(
                ClaimTypes.NameIdentifier));

    var result =
        await _service
            .GetUserTransactionsAsync(
                userId,
                queryParams);

    return Ok(result);
}

    [HttpPost]
public async Task<IActionResult>
    CreateTransaction(
        CreateTransactionDto dto)
{
    var userId =
        int.Parse(
            User.FindFirstValue(
                ClaimTypes.NameIdentifier));

    var result =
        await _service
            .CreateTransactionAsync(
                userId,
                dto);

    return Ok(result);
}
    }
}