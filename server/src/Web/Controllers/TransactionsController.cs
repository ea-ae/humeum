using Application.Transactions.Commands.AddTransaction;
using Application.Transactions.Queries;
using Application.Transactions.Queries.GetUserTransactions;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

[Route("api/v1/users/{user}/profiles/{profile}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "CanHandleUserData")]
[ValidationExceptionFilter]
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

    [HttpGet("{transaction}")]
    public IActionResult Get() {
        return StatusCode(StatusCodes.Status503ServiceUnavailable);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddTransactionCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { command.User, command.Profile, Transaction = id }, null);
    }
}
