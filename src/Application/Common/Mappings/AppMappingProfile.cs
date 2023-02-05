using Application.Transactions.Queries.GetUserTransactions;

using Domain.Entities;

using AutoMapper;

namespace Application.Common.Mappings;

internal class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Transaction, TransactionDto>();
    }
}
