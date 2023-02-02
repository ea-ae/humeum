using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IAppDbContext {
    DbSet<Transaction> Transactions { get; set; }
    DbSet<TransactionType> TransactionTypes { get; set; }
    DbSet<TimeUnit> TransactionTimescales { get; set; }
}
