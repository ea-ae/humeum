using Application.Transactions.Commands.AddTransaction;
using Application.Transactions.Queries.GetUserTransactions;

using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("transactions")]
[ApiController]
public class TransactionController : ControllerBase {
    [HttpGet("")]
    public List<TransactionDto> Index([FromServices] GetUserTransactionsQueryHandler query) {
        var transactions = query.Handle().Result;
        return transactions;
    }

    [HttpPost("")]
    public int Add([FromServices] AddTransactionCommandHandler command, int amount) {
        var result = command.Handle(new AddTransactionCommand { Amount = amount }).Result;
        return result;
    }
}
