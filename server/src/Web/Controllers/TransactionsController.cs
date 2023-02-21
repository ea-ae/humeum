using Application.Transactions.Commands.AddTransaction;
using Application.Transactions.Commands.DeleteTransaction;
using Application.Transactions.Queries;
using Application.Transactions.Queries.GetTransaction;
using Application.Transactions.Queries.GetTransactions;

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
    public async Task<List<TransactionDto>> GetTransactions(GetTransactionsQuery query) {
        var transactions = await _mediator.Send(query);
        return transactions;
    }

    [HttpGet("{transaction}")]
    public async Task<IActionResult> GetTransaction(GetTransactionQuery query) {
        var transaction = await _mediator.Send(query);
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> AddTransaction(AddTransactionCommand command) {
        int id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTransaction), new { command.User, command.Profile, Transaction = id }, null);
    }

    [HttpDelete("{transaction}")]
    public async Task<IActionResult> DeleteTransaction(DeleteTransactionCommand command) {
        await _mediator.Send(command);
        return NoContent();
    }
}
