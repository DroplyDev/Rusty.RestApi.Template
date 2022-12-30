using System.Linq.Expressions;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Repositories.BaseRepository;

namespace Rusty.Template.Infrastructure.Repositories.AppDbRepo;

/// <summary>
///     The app db repo class
/// </summary>
/// <seealso cref="BaseGenericRepo{AppDbContext,TEntity}" />
public abstract class AppDbRepo<TEntity> : BaseGenericRepo<AppDbContext, TEntity> where TEntity : class
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AppDbRepo" /> class
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="defaultOrderBy">The default order by</param>
    protected AppDbRepo(AppDbContext context, Expression<Func<TEntity, object>> defaultOrderBy) : base(context,
        defaultOrderBy)
    {
    }
}