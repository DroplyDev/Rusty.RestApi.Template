using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Repositories.BaseRepo;

namespace Rusty.Template.Infrastructure.Repositories.AppDbRepo;

/// <summary>
///     The user repo class
/// </summary>
/// <seealso cref="BaseGenericRepo{TContext,TEntity}" />
/// <seealso cref="IUserRepo" />
public class UserRepo : AppDbRepo<User>, IUserRepo
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UserRepo" /> class
    /// </summary>
    /// <param name="context">The context</param>
    public UserRepo(AppDbContext context) : base(context, item => item.UserName)
    {
    }

    /// <summary>
    ///     Gets the by username using the specified username
    /// </summary>
    /// <param name="username">The username</param>
    /// <returns>A task containing the user</returns>
    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await FirstOrDefaultAsync(item => item.UserName == username);
    }
}