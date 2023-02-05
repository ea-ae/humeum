using Application.Transactions.Queries.GetUserTransactions;

using Domain.Entities;

using AutoMapper;
using Domain.ValueObjects;

namespace Application.Common.Mappings;

internal class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Transaction, TransactionDto>();
    }
}
