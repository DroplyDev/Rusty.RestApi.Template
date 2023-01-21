#region

using Serilog.Events;

#endregion

namespace Rusty.Template.Domain.Exceptions.Entity;

public class EntityNotFoundByIdException<TEntity> : BaseEntityException<TEntity> where TEntity : class
{
	public EntityNotFoundByIdException(object id) : base(
		$@"{typeof(TEntity).Name} with id {id} was not found", 404, LogEventLevel.Warning)
	{
		Id = id;
	}


	public object Id { get; }
}