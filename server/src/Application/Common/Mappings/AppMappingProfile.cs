using Application.Profiles.Queries;
using Application.Transactions.Queries;

using AutoMapper;

using Domain.TransactionAggregate;

namespace Application.Common.Mappings;

public class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Transaction, TransactionDto>();
        CreateMap<Domain.ProfileAggregate.Profile, ProfileDto>();
    }
}
