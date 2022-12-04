using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

public class EntityValidationException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    public EntityValidationException() : base("Entity validation failed", 400)
    {
    }
}