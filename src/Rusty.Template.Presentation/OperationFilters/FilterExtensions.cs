#region

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

public static class FilterExtensions
{
	public static bool TryAddResponse<TContent>(this OpenApiOperation operation, OperationFilterContext context,
												int statusCode, string description,
												string mediaType = "application/json")
	{
		var responseType = typeof(TContent);
		if (!context.SchemaRepository.Schemas.TryGetValue(responseType.Name, out var responseSchema))
			responseSchema = context.SchemaGenerator.GenerateSchema(responseType, context.SchemaRepository);

		responseSchema.Reference = new OpenApiReference
		{
			Type = ReferenceType.Schema,
			Id = responseType.Name
		};
		var response = new OpenApiResponse
		{
			Description = description
		};
		response.Content.TryAdd(mediaType, new OpenApiMediaType {Schema = responseSchema});
		return operation.Responses.TryAdd(statusCode.ToString(), response);
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