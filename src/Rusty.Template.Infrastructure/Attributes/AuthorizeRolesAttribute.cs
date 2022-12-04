using Microsoft.AspNetCore.Authorization;

namespace Rusty.Template.Infrastructure.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}