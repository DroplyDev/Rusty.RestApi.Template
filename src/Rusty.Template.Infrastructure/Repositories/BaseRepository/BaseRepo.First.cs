using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Domain;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

public partial class BaseRepo<TEntity> where TEntity : BaseEntity
{
    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).FirstOrDefault(expression);
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return await IncludeIfNotNull(includes).FirstOrDefaultAsync(expression);
    }

    public virtual TEntity First(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes).First(expression);
    }

    public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return await IncludeIfNotNull(includes).FirstAsync(expression);
    }
}