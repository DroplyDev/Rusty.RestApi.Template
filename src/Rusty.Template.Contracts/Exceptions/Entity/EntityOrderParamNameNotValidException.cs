#region

using Serilog.Events;

#endregion

namespace Rusty.Template.Contracts.Exceptions.Entity;

public class EntityOrderParamNameNotValidException<TEntity> : BaseEntityException<TEntity> where TEntity : class
{
	public EntityOrderParamNameNotValidException(string message, string field) : base(message, 400,
		LogEventLevel.Warning)
	{
		Field = field;
	}


	public EntityOrderParamNameNotValidException(string field) : base(
		$@"Order by procedure failed. Field: {field} was not found in in response dto", 400, LogEventLevel.Warning)
	{
		Field = field;
	}


	public string Field { get; }
}