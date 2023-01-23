namespace Rusty.Template.Contracts.Responses;

public sealed record ValidationErrorResponse
{
	public string Message { get; set; } = null!;
	public List<ValidationError> Errors { get; set; } = null!;
}

public class ValidationError
{
	public string Field { get; set; } = null!;
	public string Message { get; set; } = null!;
}