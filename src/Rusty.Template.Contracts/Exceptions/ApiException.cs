#region

using System.Collections;
using System.Text.Json;
using Serilog.Events;

#endregion

namespace Rusty.Template.Contracts.Exceptions;

public class ApiException : Exception
{
	private readonly LogEventLevel _logLevel = LogEventLevel.Fatal;


	public ApiException(string description, int statusCode, LogEventLevel logLevel)
	{
		Description = description;
		StatusCode = statusCode;
		_logLevel = logLevel;
	}


	public ApiException()
	{
	}


	public int StatusCode { get; } = 500;


	public string Description { get; } = "Unhandled exception occured";

	public LogEventLevel GetLevel()
	{
		return _logLevel;
	}

	public virtual string ToJsonResponse()
	{
		return JsonSerializer.Serialize(new
		{
			ErrorMessage = Description,
			StatusCode
		});
	}

	public virtual IDictionary GetLogData()
	{
		var data = Data;
		data.Remove("Identifier");
		data.Remove("TargetSite");
		data.Remove("HelpLink");
		data.Remove("HResult");
		data.Remove("StackTrace");
		data.Remove("Source");

		return data;
	}
}