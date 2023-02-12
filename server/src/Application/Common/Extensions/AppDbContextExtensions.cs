using Application.Common.Interfaces;

using Domain.Common;

namespace Application.Common.Extensions;

internal static class AppDbContextExtensions {
    //public static T ToEnumerationEntityByCode<T>(this string code, IAppDbContext context) where T : EnumerationEntity {
    //    T enumEntity = EnumerationEntity.GetByCode<T>(code);
    //    context.Set<T>().Attach(enumEntity);
    //    return enumEntity;
    //}

    public static T GetEnumerationEntityByCode<T>(this IAppDbContext context, string code) where T : Enumeration {
        T enumEntity = Enumeration.GetByCode<T>(code);
        context.Set<T>().Attach(enumEntity);
        return enumEntity;
    }
}
