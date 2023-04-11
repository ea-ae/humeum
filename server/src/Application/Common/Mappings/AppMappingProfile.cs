using Application.Assets.Queries;
using Application.Profiles.Queries;
using Application.TaxSchemes.Queries;
using Application.TransactionCategories.Queries;
using Application.Transactions.Queries;
using Application.Users.Queries;

using AutoMapper;

using Domain.AssetAggregate;
using Domain.TaxSchemeAggregate;
using Domain.TransactionAggregate;
using Domain.TransactionCategoryAggregate;
using Domain.UserAggregate;

namespace Application.Common.Mappings;

public class AppMappingProfile : Profile {
    public AppMappingProfile() {
        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.TaxScheme,
                       o => o.MapFrom(src => new TransactionDto.BriefRelatedResourceDto { Id = src.TaxScheme.Id, Name = src.TaxScheme.Name }))
            .ForMember(dest => dest.Asset,
                       o => o.MapFrom(src => src.Asset == null ? null : new TransactionDto.BriefRelatedResourceDto { Id = src.Asset.Id, Name = src.Asset.Name }))
            .ForMember(dest => dest.Categories,
                       o => o.MapFrom(src => src.Categories.Select(
                           c => new TransactionDto.BriefRelatedResourceDto { Id = c.Id, Name = c.Name })));

        CreateMap<TransactionCategory, CategoryDto>()
            .ForMember(dest => dest.Default,
                       o => o.MapFrom(src => src.ProfileId == null));

        CreateMap<Asset, AssetDto>()
            .ForMember(dest => dest.Type,
                       o => o.MapFrom(src => new AssetDto.AssetTypeDto { Id = src.TypeId, Name = src.Type.Name }))
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
