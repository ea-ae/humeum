using Application.Profiles.Queries.GetUserProfileDetails;
using Application.Transactions.Queries.GetUserTransactions;

using AutoMapper;

using Domain.TransactionAggregate;

namespace Application.Common.Mappings;

internal class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Transaction, TransactionDto>();
        CreateMap<Domain.ProfileAggregate.Profile, ProfileDto>();
    }
}
