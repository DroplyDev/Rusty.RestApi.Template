#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace Rusty.Template.Contracts.Responses;

public sealed class ApiValidationResult : ValidationResult
{
	/// <summary>
	///     Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" />
	///     class by using a <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> object.
	/// </summary>
	/// <param name="validationResult">The validation result object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///     <paramref name="validationResult" /> is <see langword="null" />.
	/// </exception>
	public ApiValidationResult(ValidationResult validationResult) : base(validationResult)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" />
	///     class by using an error message.
	/// </summary>
	/// <param name="errorMessage">The error message.</param>
	public ApiValidationResult(string? errorMessage) : base(errorMessage)
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" />
	///     class by using an error message and a list of members that have validation errors.
	/// </summary>
	/// <param name="errorMessage">The error message.</param>
	/// <param name="memberNames">The list of member names that have validation errors.</param>
	public ApiValidationResult(string? errorMessage, IEnumerable<string>? memberNames) : base(errorMessage, memberNames)
	{
	}
}