using Application.Profiles.Queries;
using Application.TransactionCategories.Queries;
using Application.Transactions.Queries;
using Application.Users.Queries;

using AutoMapper;

using Domain.TransactionAggregate;
using Domain.TransactionCategoryAggregate;
using Domain.UserAggregate;

namespace Application.Common.Mappings;

public class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<TransactionCategory, CategoryDto>()
            .ForMember(dest => dest.Default, o => o.MapFrom(src => src.ProfileId == null));
        CreateMap<Transaction, TransactionDto>();
            //.ForMember(dest => dest.Categories, o => o.MapFrom(src => src.Categories.Select(c => c.Name)));


        CreateMap<Domain.ProfileAggregate.Profile, ProfileDto>();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Profiles, o => o.MapFrom(src => src.Profiles.Select(p => new { p.Id, p.Name })));
    }
}
