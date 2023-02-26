using Application.Profiles.Queries;
using Application.Transactions.Queries;
using Application.Users.Queries;

using AutoMapper;

using Domain.TransactionAggregate;
using Domain.UserAggregate;

namespace Application.Common.Mappings;

public class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.Categories, o => o.MapFrom(src => src.Categories.Select(c => c.Name)));

        CreateMap<Domain.ProfileAggregate.Profile, ProfileDto>();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Profiles, o => o.MapFrom(src => src.Profiles.Select(p => p.Id)));
    }
}
