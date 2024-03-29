#region

using Serilog.Events;

#endregion

namespace Rusty.Template.Domain.Exceptions.Entity;

public class EntityOrderParamNameNotValidException<TEntity> : BaseEntityException<TEntity> where TEntity : class
{
	public EntityOrderParamNameNotValidException(string message, string field) : base(message, 400,
		LogEventLevel.Warning)
	{
		Field = field;
	}


	public EntityOrderParamNameNotValidException(string field) : base(
		$@"You can not sort by {field}. It does not exist in response dto", 400, LogEventLevel.Warning)
	{
		Field = field;
	}


	public string Field { get; }
}