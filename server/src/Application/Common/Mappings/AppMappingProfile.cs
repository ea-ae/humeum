using Application.Assets.Queries;
using Application.Profiles.Queries;
using Application.TaxSchemes.Queries;
using Application.TransactionCategories.Queries;
using Application.Transactions.Queries;
using Application.Users.Queries;

using AutoMapper;

using Domain.AssetAggregate;
using Domain.TaxSchemeAggregate;
using Domain.TaxSchemeAggregate.ValueObjects;
using Domain.TransactionAggregate;
using Domain.TransactionAggregate.ValueObjects;
using Domain.TransactionCategoryAggregate;
using Domain.UserAggregate;

namespace Application.Common.Mappings;

public class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<TransactionCategory, CategoryDto>()
            .ForMember(dest => dest.Default, 
                       o => o.MapFrom(src => src.ProfileId == null));
        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.Categories,
                       o => o.MapFrom(src => src.Categories.Select(
                           c => new TransactionDto.BriefTransactionCategory { Id = c.Id, Name = c.Name})));

        CreateMap<AssetType, AssetTypeDto>();
        CreateMap<Asset, AssetDto>()
            .ForMember(dest => dest.Default,
                       o => o.MapFrom(src => src.ProfileId == null));

        CreateMap<TaxScheme, TaxSchemeDto>();

        CreateMap<Domain.ProfileAggregate.Profile, ProfileDto>();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Profiles, 
                       o => o.MapFrom(src => src.Profiles.Select(
                           p => new UserDto.BriefProfile { Id = p.Id, Name = p.Name })));
    }
}
