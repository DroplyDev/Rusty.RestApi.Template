#region

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace Rusty.Template.Presentation.OperationFilters;

/// <summary>
/// Filter extensions
/// </summary>
public static class FilterExtensions
{
	/// <summary>Tries the add response.</summary>
	/// <typeparam name="TContent">The type of the content.</typeparam>
	/// <param name="operation">The operation.</param>
	/// <param name="context">The context.</param>
	/// <param name="statusCode">The status code.</param>
	/// <param name="description">The description.</param>
	/// <param name="mediaType">Type of the media.</param>
	/// <returns></returns>
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
		response.Content.TryAdd(mediaType, new OpenApiMediaType { Schema = responseSchema });
		return operation.Responses.TryAdd(statusCode.ToString(), response);
	}

	/// <summary>Tries the add response.</summary>
	/// <param name="operation">The operation.</param>
	/// <param name="statusCode">The status code.</param>
	/// <param name="description">The description.</param>
	/// <returns></returns>
	public static bool TryAddResponse(this OpenApiOperation operation, int statusCode, string description)
	{
		return operation.Responses.TryAdd(statusCode.ToString(),
			new OpenApiResponse
			{
				Description = description
			});
	}
}