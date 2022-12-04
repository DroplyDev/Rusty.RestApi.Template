using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Domain;

namespace Rusty.Template.Infrastructure.Repositories.BaseRepository;

public partial class BaseRepo<TEntity> where TEntity : BaseEntity
{
    public IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes);
    }

    public IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        return IncludeIfNotNull(includes)
            .OrderByWithDirection(orderBy, orderDirection);
    }
}