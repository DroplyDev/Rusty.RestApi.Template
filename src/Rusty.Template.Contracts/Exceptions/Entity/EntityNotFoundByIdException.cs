using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

public class EntityNotFoundByIdException<TEntity> : BaseEntityException<TEntity> where TEntity : BaseEntity
{
    public EntityNotFoundByIdException(int identifier) : base(
        $"{typeof(TEntity).Name} with id {identifier} was not found", 404)
    {
        Identifier = identifier;
    }

    public int Identifier { get; }
}