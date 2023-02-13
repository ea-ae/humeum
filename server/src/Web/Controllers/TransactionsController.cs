using Application.Transactions.Commands.AddTransaction;
using Application.Transactions.Queries.GetUserTransactions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

[Route("api/v1/users/{user}/profiles/{profile}/[controller]")]
[CsrfXHeaderFilter]
[ApiController]
public class TransactionsController : ControllerBase {
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<List<TransactionDto>> GetAll(GetUserTransactionsQuery query) {
        var transactions = await _mediator.Send(query);
        return transactions;
    }

    [HttpGet("{id}")]
    public IActionResult Get() {
        return StatusCode(StatusCodes.Status503ServiceUnavailable);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int user, AddTransactionCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { user, id }, null);
    }
}
