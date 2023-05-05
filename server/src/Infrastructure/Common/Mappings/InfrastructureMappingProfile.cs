using Application.V1.Transactions.Queries;

using AutoMapper;
using Domain.V1.UserAggregate;
using Infrastructure.Auth;

namespace Application.Common.Mappings;

public class InfrastructureMappingProfile : Profile {
    public InfrastructureMappingProfile() {
        CreateMap<User, ApplicationUser>()
            .ForMember(dest => dest.DisplayName,
                       o => o.MapFrom(src => src.Username));
    }
}
