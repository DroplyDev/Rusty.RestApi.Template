#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Rusty.Template.Infrastructure.Database;

public static class ContextExtensions
{
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