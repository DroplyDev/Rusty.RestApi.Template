using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

public class OrderByStatementNotFoundException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    public OrderByStatementNotFoundException(string message) : base(message, 400)
    {
    }
}