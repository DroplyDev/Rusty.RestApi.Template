#region

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation;

public static class FilterExtensions
{
	public static Dictionary<string, OpenApiMediaType> SetTypeForContent<TContent>(
		this OperationFilterContext context, string mediaType = "application/json") where TContent : new()
	{
		var type = new TContent().GetType();
		context.SchemaGenerator.GenerateSchema(type, context.SchemaRepository);
		var schema = context.SchemaRepository.Schemas[type.Name];
		return new Dictionary<string, OpenApiMediaType>
		{
			{
				mediaType, new OpenApiMediaType
				{
					Schema = schema
				}
			}
		};
	}

	public static bool TryAddResponse<TContent>(this OpenApiOperation operation, OperationFilterContext context,
												int statusCode, string description,
												string mediaType = "application/json") where TContent : new()
	{
		return operation.Responses.TryAdd(statusCode.ToString(),
			new OpenApiResponse
			{
				Description = description,
				Content = context.SetTypeForContent<TContent>()
			});
	}

	public static bool TryAddResponse(this OpenApiOperation operation, int statusCode, string description)
	{
		return operation.Responses.TryAdd(statusCode.ToString(),
			new OpenApiResponse
			{
				Description = description
			});
	}
}