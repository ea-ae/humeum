using Application.Transactions.Commands.AddTransaction;
using Application.Transactions.Queries.GetUserTransactions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/v1/users/{user}/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase {
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<List<TransactionDto>> Index([FromRoute] GetUserTransactionsQuery query) {
        return await _mediator.Send(query);
    }

    [HttpPost]
    public async Task<int> Add([FromQuery] AddTransactionCommand command) {
        return await _mediator.Send(command);
    }
}
