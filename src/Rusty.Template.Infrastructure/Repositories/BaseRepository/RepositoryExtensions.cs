using System.Linq.Expressions;
using Rusty.Template.Contracts.Exceptions;
using Rusty.Template.Contracts.SubTypes;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

public static class RepositoryExtensions
{
    public static IOrderedQueryable<TSource> OrderByWithDirection<TSource>(
        this IQueryable<TSource> query, string propertyName, OrderDirection direction)
    {
        var entityType = typeof(TSource);
        //Create x=>x.PropName
        var propertyInfo = entityType.GetProperty(propertyName);
        var arg = Expression.Parameter(entityType, "x");
        var property = Expression.Property(arg, propertyName);
        var selector = Expression.Lambda(property, arg);

        //Get System.Linq.Queryable.OrderByDescending() method.
        var enumerableType = typeof(Queryable);
        var method = enumerableType.GetMethods()
            .Where(m => m.Name == (direction == OrderDirection.Asc ? "OrderBy" : "OrderByDescending") &&
                        m.IsGenericMethodDefinition)
            .Single(m =>
            {
                var parameters = m.GetParameters().ToList();
                //Put more restriction here to ensure selecting the right overload                
                return parameters.Count == 2; //overload that has 2 parameters
            });
        //The linq's OrderByDescending<TSource, TKey> has two generic types, which provided here
        var genericMethod = method
            .MakeGenericMethod(entityType, propertyInfo?.PropertyType!);

        /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
          Note that we pass the selector as Expression to the method and we don't compile it.
          By doing so EF can extract "order by" columns and generate SQL for it.*/
        var newQuery = (IOrderedQueryable<TSource>)genericMethod
            .Invoke(genericMethod, new object[] { query, selector })!;
        return newQuery;
    }

    public static IQueryable<TSource> SkipWithValidation<TSource>(this IQueryable<TSource> query, int offset)
    {
        return offset < 0 ? throw new NegativeValueException("Offset must be non negative") : query.Skip(offset);
    }

    public static IQueryable<TSource> TakeWithValidation<TSource>(this IQueryable<TSource> query, int limit)
    {
        return limit < 0 ? throw new NegativeValueException("Limit must be non negative") : query.Take(limit);
    }
}