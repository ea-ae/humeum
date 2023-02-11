﻿using Application.Transactions.Commands.AddTransaction;
using Application.Transactions.Queries.GetUserTransactions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Web.Filters;

namespace Web.Controllers;

[Route("api/v1/users/{user}/[controller]")]
[CsrfXHeaderFilter]
[ApiController]
public class TransactionsController : ControllerBase {
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<List<TransactionDto>> Index(GetUserTransactionsQuery query) {
        return await _mediator.Send(query);
    }

    [HttpPost]
    public async Task<int> Add(AddTransactionCommand command) {
        return await _mediator.Send(command);
    }
}