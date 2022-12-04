using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Rusty.Template.Contracts.SubTypes;
using Rusty.Template.Domain;

namespace Rusty.Template.Application.Repositories;

public interface IBaseRepo<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);
    Task CreateNoSaveAsync(TEntity entity);
    Task UpdateStateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    void UpdateNoSave(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(int id);
    void DeleteNoSave(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    void DeleteNoSaveRange(IEnumerable<TEntity> entities);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(TEntity entity);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> IsEmptyAsync();

    Task SaveChangesAsync();

    //First
    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    TEntity First(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    //Get All
    IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    IQueryable<TEntity> GetAll(string orderBy, OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    //Pagination
    IQueryable<TEntity> Paginate(int skipItems, int takeItems, Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    IQueryable<TEntity> Paginate(int skipItems, int takeItems,
        Expression<Func<TEntity, bool>>? expression,
        string orderBy, OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    //Where
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, string orderBy,
        OrderDirection orderDirection,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);
}