﻿using System.ComponentModel.DataAnnotations;

using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;

using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;

namespace Application.Transactions.Commands.AddTransaction;

public record AddTransactionCommand : ICommand<int> {
    [Required] public required int User { get; init; }
    [Required] public required int Profile { get; init; }

    public string? Name { get; init; }
    public string? Description { get; init; }
    [Required] public decimal? Amount { get; init; }
    [Required] public required string Type { get; init; }
    [Required] public DateOnly? PaymentStart { get; init; }
    [Required] public required int? TaxScheme { get; init; }
    public int? Asset { get; init; }

    public DateOnly? PaymentEnd { get; init; }
    public string? TimeUnit { get; init; }
    public int? TimesPerCycle { get; init; }
    public int? UnitsInCycle { get; init; }
}

public class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand, int> {
    private readonly IAppDbContext _context;

    public AddTransactionCommandHandler(IAppDbContext context) => _context = context;

    public async Task<int> Handle(AddTransactionCommand request, CancellationToken token = default) {
        // validation

        List<object?> recurringTransactionFields = new() { // fields required for recurrent transactions
            request.PaymentEnd,
            request.TimeUnit,
            request.TimesPerCycle,
            request.UnitsInCycle
        };

        int recurringTransactionFieldCount = recurringTransactionFields.Count(field => field is not null);
        bool isRecurringTransaction = recurringTransactionFieldCount == recurringTransactionFields.Count;

        if (!isRecurringTransaction && recurringTransactionFieldCount > 0) {
            throw new ApplicationValidationException("Fields for recurrent transactions were only partially specified.");
        }

        _context.AssertUserOwnsProfile(request.User, request.Profile);

        var taxSchemeExists = _context.TaxSchemes.Any(ts => ts.Id == request.TaxScheme && ts.DeletedAt == null);
        if (!taxSchemeExists) {
            throw new NotFoundValidationException("Tax scheme with given ID was not found.");
        }

        if (request.Asset is not null) {
            // valid asset IDs are those provided either by default or created in a profile
            //var validAssetIds = _context.Assets.Where(a => (a.ProfileId == request.Profile || a.ProfileId == null) && a.DeletedAt == null)
            //                                   .Select(a => a.Id)
            //                                   .ToList();
            //if (!validAssetIds.Contains((int)request.Asset)) {
            //    throw new NotFoundValidationException("Asset with given ID was not found for profile.");
            //}
            var assetExists = _context.Assets.Any(a => (a.ProfileId == request.Profile || a.ProfileId == null)
                                                       && a.Id == request.Asset
                                                       && a.DeletedAt == null);
            if (!assetExists) {
                throw new NotFoundValidationException("Asset with given ID was not found for profile.");
            }
        }

        // handling

        var transactionType = _context.GetEnumerationEntityByCode<TransactionType>(request.Type);

        Transaction transaction;
        if (isRecurringTransaction) {
            var timeUnit = _context.GetEnumerationEntityByCode<TimeUnit>(request.TimeUnit!);
            var paymentPeriod = new TimePeriod((DateOnly)request.PaymentStart!, (DateOnly)request.PaymentEnd!);
            var paymentFrequency = new Frequency(timeUnit, (int)request.TimesPerCycle!, (int)request.UnitsInCycle!);
            var paymentTimeline = new Timeline(paymentPeriod, paymentFrequency);
            transaction = new Transaction(request.Name, request.Description, (decimal)request.Amount!, transactionType,
                                          paymentTimeline, request.Profile!, (int)request.TaxScheme!, request.Asset);
        } else {
            var timeline = new Timeline(new TimePeriod((DateOnly)request.PaymentStart!));
            transaction = new Transaction(request.Name, request.Description, (decimal)request.Amount!, transactionType,
                                          timeline, request.Profile!, (int)request.TaxScheme!, request.Asset);
        }

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(token);

        return transaction.Id;
    }
}
