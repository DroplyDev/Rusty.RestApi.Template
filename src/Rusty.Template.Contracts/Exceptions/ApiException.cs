using Serilog.Events;

namespace Rusty.Template.Contracts.Exceptions;

/// <summary>
///     The api exception class
/// </summary>
/// <seealso cref="Exception" />
public class ApiException : Exception
{
	/// <summary>
	///     Gets or sets the value of the log level
	/// </summary>
	private readonly LogEventLevel _logLevel = LogEventLevel.Fatal;

	/// <summary>
	///     Initializes a new instance of the <see cref="ApiException" /> class
	/// </summary>
	/// <param name="description"></param>
	/// <param name="statusCode">The status code</param>
	/// <param name="logLevel"></param>
	public ApiException(string description, int statusCode, LogEventLevel logLevel)
	{
		Description = description;
		StatusCode = statusCode;
		_logLevel = logLevel;
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="ApiException" /> class
	/// </summary>
	public ApiException()
	{
	}

	/// <summary>
	///     Gets the value of the status code
	/// </summary>
	public int StatusCode { get; } = 500;

	/// <summary>
	///     Gets the value of the message
	/// </summary>
	public string Description { get; } = "Unhandled exception occured";

	public LogEventLevel GetLevel()
	{
		return _logLevel;
	}

	public override string ToString()
	{
		return Description;
	}
}