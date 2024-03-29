﻿using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Common.Models;
using Domain.V1.ProfileAggregate;
using Domain.V1.TransactionAggregate;

namespace Domain.V1.TransactionCategoryAggregate;

/// <summary>
/// Optional many-to-many categories/tags for transactions. Custom categories can
/// be created by profiles for filtering and/or statistics.
/// </summary>
public class TransactionCategory : TimestampedEntity, IOptionalProfileEntity
{
    string _name = null!;
    public string Name
    {
        get => _name;
        private set
        {
            if (value.Length == 0)
            {
                throw new DomainException(new ArgumentException("Name cannot be empty.", nameof(Name)));
            }
            _name = value;
        }
    }

    public int? ProfileId { get; private set; }
    public Profile? Profile { get; private set; }

    HashSet<Transaction> _transactions = null!;
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public TransactionCategory(string name, Profile? profile = null) : this(name, profile?.Id)
    {
        Profile = profile;
    }

    public TransactionCategory(string name, int? profileId = null)
    {
        Name = name;
        ProfileId = profileId;
    }

    public TransactionCategory(int categoryId, string name)
    {
        Id = categoryId;
        Name = name;
    }

    private TransactionCategory() { }
}
