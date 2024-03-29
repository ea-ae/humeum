﻿using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.V1.TaxSchemeAggregate;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionAggregate.ValueObjects;
using Shared.Interfaces;
using Shared.Models;

namespace Application.V1.Transactions.Commands.Common;

internal static class TransactionCommandExtensions
{
    /// <summary>Data retrieved during the transaction field validation process. To be used in transaction creation or replacement.</summary>
    public record ValidatedTransactionData(TransactionType TransactionType, Timeline PaymentTimeline);

    /// <summary>
    /// Attempts to validate and prepare all of the required fields to create or replace a transaction.
    /// </summary>
    /// <param name="command">Command that contains all of the required transaction fields.</param>
    /// <returns>Result containing either prepared transaction data or validation errors.</returns>
    public static IResult<ValidatedTransactionData, IBaseException> ValidateTransactionFields(this ITransactionFields request, IAppDbContext context)
    {
        // create a builder to collect validation errors

        var builder = new Result<ValidatedTransactionData, IBaseException>.Builder();

        // validate that the fields for recurring transactions were provided either fully or not at all

        List<object?> recurringTransactionFields = new() {
            request.PaymentEnd,
            request.TimeUnit,
            request.TimesPerCycle,
            request.UnitsInCycle
        };
        var isRecurringTransaction = recurringTransactionFields.AssertOptionalFieldSetValidity();
        builder.AddResultErrors(isRecurringTransaction);

        // validate that the tax scheme exists

        var taxSchemeExists = context.TaxSchemes.Any(ts => ts.Id == request.TaxScheme && ts.DeletedAt == null);
        if (!taxSchemeExists)
        {
            builder.AddError(new NotFoundValidationException(typeof(TaxScheme)));
        }

        // validate that the asset exists if one was specified

        if (request.Asset is not null)
        {
            var assetExists = context.Assets.Any(a => (a.ProfileId == request.Profile || a.ProfileId == null)
                                                      && a.Id == request.Asset
                                                      && a.DeletedAt == null);
            if (!assetExists)
            {
                builder.AddError(new NotFoundValidationException(typeof(Transaction)));
            }
        }

        // validate and get the transaction type by code

        var transactionType = context.GetEnumerationEntityByCode<TransactionType>(request.Type);
        builder.AddResultErrors(transactionType);

        // if the fields were specified partially, transaction creation cannot proceed; return failed result early

        if (isRecurringTransaction.Failure)
        {
            return builder.Build();
        }

        // create a payment timeline value object for the transaction; return failed result early if needed

        IResult<Timeline, IBaseException> paymentTimeline;
        var start = DateOnly.FromDateTime((DateTime)request.PaymentStart!);


        if (isRecurringTransaction.Unwrap())
        {
            var end = DateOnly.FromDateTime((DateTime)request.PaymentEnd!);
            var paymentPeriod = TimePeriod.Create(start, end);
            builder.AddResultErrors(paymentPeriod);

            var timeUnit = context.GetEnumerationEntityByCode<TimeUnit>(request.TimeUnit!);
            builder.AddResultErrors(timeUnit);

            if (paymentPeriod.Failure || timeUnit.Failure)
            {
                return builder.Build();
            }

            var paymentFrequency = Frequency.Create(timeUnit.Unwrap(), (int)request.TimesPerCycle!, (int)request.UnitsInCycle!);
            if (paymentFrequency.Failure)
            {
                return builder.AddResultErrors(paymentFrequency).Build();
            }

            paymentTimeline = Timeline.Create(paymentPeriod.Unwrap(), paymentFrequency.Unwrap());
        }
        else
        {
            var paymentPeriod = TimePeriod.Create(start);
            if (paymentPeriod.Failure)
            {
                return builder.AddResultErrors(paymentPeriod).Build();
            }

            paymentTimeline = Timeline.Create(paymentPeriod.Unwrap());
        }

        builder.AddResultErrors(paymentTimeline);

        // if there were any validation errors, return them

        if (builder.HasErrors)
        {
            return builder.Build();
        }

        // otherwise, return validated data required for transaction

        return builder.AddValue(new ValidatedTransactionData(transactionType.Unwrap(), paymentTimeline.Unwrap())).Build();
    }
}
