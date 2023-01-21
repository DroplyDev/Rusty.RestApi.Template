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
		// var responseProperties = responseType.GetProperties();
		// var responseSchema = new OpenApiSchema
		// {
		// 	Reference = new OpenApiReference()
		// 	{
		// 		Id = responseType.Name,
		// 		Type = ReferenceType.Schema
		// 	},
		// 	Type = "object",
		// 	Properties = new Dictionary<string, OpenApiSchema>()
		// };
		// foreach (var property in responseProperties)
		// {
		// 	responseSchema.Properties.Add(property.Name, new OpenApiSchema
		// 		{
		// 			Type =SimpleTypeParser(property.PropertyType.Name)
		// 		}
		// 	);
		// }
		//
		// context.SchemaRepository.Schemas.TryAdd(responseType.Name, responseSchema);

		if (!context.SchemaRepository.Schemas.TryGetValue(responseType.Name, out var responseSchema))
		{
			context.SchemaGenerator.GenerateSchema(responseType, context.SchemaRepository);
			responseSchema = context.SchemaRepository.Schemas[responseType.Name];
		}

		var response = new OpenApiResponse
		{
			Description = description
		};
		response.Content.TryAdd(mediaType, new OpenApiMediaType { Schema = responseSchema });
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

	private static string SimpleTypeParser(string type)
	{
		switch (type.ToLower())
		{
			case "int16":
			case "uint16":
			case "int32":
			case "uint32":
			case "int64":
			case "uint64":
				return "integer";
			case "single":
			case "float":
			case "double":
				return "number";
			case "char":
				return "string";
			case "boolean":
				return "boolean";
		}

		return "object";
	}
}