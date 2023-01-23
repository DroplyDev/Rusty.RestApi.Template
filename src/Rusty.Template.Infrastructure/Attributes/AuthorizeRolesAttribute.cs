#region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace Rusty.Template.Infrastructure.Attributes;

public sealed class AuthorizeRolesAttribute : AuthorizeAttribute
{
	public AuthorizeRolesAttribute(params string[] roles)
	{
		Roles = string.Join(",", roles);
	}
}