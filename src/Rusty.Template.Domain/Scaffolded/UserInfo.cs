namespace Rusty.Template.Domain;

public class UserInfo
{
	public string FirstName { get; set; } = null!;

	public string? LastName { get; set; }

	public int UserId { get; set; }

	public virtual User User { get; set; } = null!;
}