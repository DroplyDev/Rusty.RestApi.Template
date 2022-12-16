using Microsoft.AspNetCore.Authorization;

namespace Rusty.Template.Infrastructure.Attributes;

/// <summary>
///     The authorize roles attribute class
/// </summary>
/// <seealso cref="AuthorizeAttribute" />
public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AuthorizeRolesAttribute" /> class
    /// </summary>
    /// <param name="roles">The roles</param>
    public AuthorizeRolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}