using Application.Common.Interfaces;

using Domain.Common;
using Domain.Entities;

namespace Application.Common;

internal static class EnumerationEntityExtensions {
    public static T ToEnumerationEntityByCode<T>(this string code, IAppDbContext context) where T : EnumerationEntity {
        T enumEntity = EnumerationEntity.GetByCode<T>(code);
        context.Set<T>().Attach(enumEntity);
        return enumEntity;
    }
}
