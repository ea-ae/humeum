using Application.Transactions.Queries;

using AutoMapper;

using Domain.UserAggregate;

using Infrastructure.Auth;

namespace Application.Common.Mappings;

public class InfrastructureMappingProfile : Profile {
    public InfrastructureMappingProfile() {
        CreateMap<User, ApplicationUser>()
            .ForMember(dest => dest.DisplayName,
                       o => o.MapFrom(src => src.Username));
    }
}
