using Application.Transactions.Queries.GetUserTransactions;

using AutoMapper;

using Domain.Entities;

namespace Application.Common.Mappings;

internal class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Transaction, TransactionDto>();
    }
}
