﻿using Domain.Common;
using Domain.Entities;

namespace Domain.ValueObjects;

public class TransactionType : Enumeration {
    public static readonly TransactionType Income = new(1, "INCOME");
    public static readonly TransactionType Expense = new(2, "EXPENSE");

    IEnumerable<Transaction> _transactions = null!;
    public IEnumerable<Transaction> Transactions => _transactions;

    private TransactionType(int id, string code) : base(id, code) { }

    private TransactionType() { }
}
