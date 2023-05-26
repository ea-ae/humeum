using Application.Users.Queries;
using Application.V1.Assets.Queries;
using Application.V1.Profiles.Queries;
using Application.V1.TaxSchemes.Queries;
using Application.V1.TransactionCategories.Queries;
using Application.V1.Transactions.Queries;
using Domain.V1.AssetAggregate;
using Domain.V1.AssetAggregate.ValueObjects;
using Domain.V1.ProfileAggregate.ValueObjects;
using Domain.V1.TaxSchemeAggregate;
using Domain.V1.TransactionAggregate;
using Domain.V1.TransactionCategoryAggregate;
using Domain.V1.UserAggregate;

using static Application.V1.Profiles.Queries.ProjectionDto;

namespace Application.Common.Mappings;

public class ApplicationMappingProfile : AutoMapper.Profile {
    public ApplicationMappingProfile() {
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
                       o => o.MapFrom(src => new AssetTypeDto { Id = src.TypeId, Name = src.Type.Name, Code = src.Type.Code }))
            .ForMember(dest => dest.Default,
                       o => o.MapFrom(src => src.ProfileId == null));

        CreateMap<AssetType, AssetTypeDto>();

        CreateMap<TaxScheme, TaxSchemeDto>();

        CreateMap<Domain.V1.ProfileAggregate.Profile, ProfileDto>();

        CreateMap<Projection, ProjectionDto>()
            .ForMember(dest => dest.TimeSeries,
                       o => o.MapFrom(src => src.TimeSeries.Select(tp => new TimePointDto() { Date = tp.Date, LiquidWorth = tp.LiquidWorth, AssetWorth = tp.AssetWorth })));

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Profiles,
                       o => o.MapFrom(src => src.Profiles.Select(
                           p => new UserDto.BriefProfile { Id = p.Id, Name = p.Name })));
    }
}
