using Application.Profiles.Queries;
using Application.Transactions.Queries;

using AutoMapper;

using Domain.TransactionAggregate;
using Domain.TransactionCategoryAggregate;

namespace Application.Common.Mappings;

public class AppMappingProfile : Profile {
    public AppMappingProfile() {
        //CreateMap<TransactionCategory, string>()
        //    .ForMember(dest => dest, o => o.MapFrom(src => src.Name));
        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.Categories, o => o.MapFrom(src => src.Categories.Select(c => c.Name)));
        CreateMap<Domain.ProfileAggregate.Profile, ProfileDto>();
    }
}
