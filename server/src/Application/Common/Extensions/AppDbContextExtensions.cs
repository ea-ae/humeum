using Application.Common.Exceptions;
using Application.Common.Interfaces;

using Domain.Common;

namespace Application.Common.Extensions;

internal static class AppDbContextExtensions {
    public static T GetEnumerationEntityByCode<T>(this IAppDbContext context, string code) where T : Enumeration {
        T enumEntity;
        try {
            enumEntity = Enumeration.GetByCode<T>(code);
        } catch (InvalidOperationException) {
            throw new ValidationException($"Incorrect enumeration code {code} for {typeof(T).Name}");
        }

        context.Set<T>().Attach(enumEntity);
        return enumEntity;
    }
}
