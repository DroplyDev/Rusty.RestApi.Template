using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Rusty.Template.Infrastructure.Database;

/// <summary>
///     The context extensions class
/// </summary>
public static class ContextExtensions
{
    /// <summary>
    ///     Applies the global filters using the specified model builder
    /// </summary>
    /// <typeparam name="T">The </typeparam>
    /// <param name="modelBuilder">The model builder</param>
    /// <param name="properyName">The propery name</param>
    /// <param name="value">The value</param>
    public static void ApplyGlobalFilters<T>(this ModelBuilder
        modelBuilder, string properyName, T value)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var foundPropery = entityType.FindProperty(properyName);
            if (foundPropery is not null && foundPropery.ClrType == typeof(T))
            {
                var newParam = Expression.Parameter(entityType.ClrType);
                var filter =
                    Expression.Lambda(
                        Expression.Equal(Expression.Property(newParam, properyName), Expression.Constant(value)),
                        newParam);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }
}