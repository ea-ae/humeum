using Domain.Common.Exceptions;
using Domain.Common.Models;
using Domain.V1.AssetAggregate;
using Domain.V1.ProfileAggregate.ValueObjects;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionCategoryAggregate;
using Shared.Interfaces;
using Shared.Models;

namespace Domain.V1.ProfileAggregate;

/// <summary>
/// Profiles are created by users to store their transactions, custom asset & taxation types,
/// transaction categories, inflation & withdrawal rates, and so on.
/// </summary>
public class Profile : TimestampedEntity {
    public int UserId { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    /// <summary>
    /// The retirement goal (calculated as sum of liquid and asset value) at which one enters the retirement stage.
    /// Upon hitting it, all pre-retirement only recurring transactions stop applying and post-retirement ones start.
    /// In addition, a constant rate will be withdrawn from the portfolio annually.
    /// </summary>
    public decimal RetirementGoal { get; private set; } = 500_000m;

    /// <summary>
    /// Rate at which we wish to withdraw from the portfolio upon hitting the retirement goal.
    /// The traditional 4% rule works well for traditional 30-year retirement windows (at 65).
    /// A more aggressive 5% is recommended by many not as worried about market crashes.
    /// A conservative 3-3.5% is often recommended for early retirees.
    /// </summary>
    public decimal WithdrawalRate { get; private set; } = 3.5m;

    public DateOnly Birthday { get; private set; } = new DateOnly(2000, 1, 1);

    HashSet<Transaction> _transactions = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    HashSet<TransactionCategory> _transactionCategories = null!;
    public IReadOnlyCollection<TransactionCategory> TransactionCategories => _transactionCategories;

    HashSet<Asset> _assets = null!;
    public IReadOnlyCollection<Asset> Assets => _assets;

    public static IResult<Profile, DomainException> Create(int userId,
                                                           string name,
                                                           string? description = null,
                                                           decimal? retirementGoal = null,
                                                           decimal? withdrawalRate = null,
                                                           DateOnly? birthday = null) {
        var builder = new Result<Profile, DomainException>.Builder();

        if (name.Length > 50) {
            builder.AddError(new DomainException("Name cannot exceed 50 characters."));
        }

        if (description == "") {
            description = null;
        }

        if (retirementGoal <= 0) {
            builder.AddError(new DomainException("Retirement goal cannot be zero or below."));
        }

        if (withdrawalRate <= 0) {
            builder.AddError(new DomainException("Withdrawal rate cannot be zero or below."));
        }

        if (birthday >= DateOnly.FromDateTime(DateTime.UtcNow)) {
            builder.AddError(new DomainException("Birthday cannot be in the future."));
        }

        var profile = new Profile() { UserId = userId, Name = name, Description = description };

        if (retirementGoal is not null) {
            profile.RetirementGoal = (decimal)retirementGoal;
        }

        if (withdrawalRate is not null) {
            profile.WithdrawalRate = (decimal)withdrawalRate;
        }

        if (birthday is not null) {
            profile.Birthday = (DateOnly)birthday;
        }

        return builder.AddValue(profile).Build();
    }

    Profile() { }

    public IResult<Projection, DomainException> GenerateProjection(DateOnly until) {
        var projection = ProjectionTimePeriod.Create(DateOnly.FromDateTime(DateTime.UtcNow), until)
            .Then(period => ProjectionSimulator.Create(Transactions, period))
            .Then(simulator => simulator.SimulateProjection((double)RetirementGoal, (double)WithdrawalRate, Birthday)); // temp retirement goal for now

        return projection;
    }
}
