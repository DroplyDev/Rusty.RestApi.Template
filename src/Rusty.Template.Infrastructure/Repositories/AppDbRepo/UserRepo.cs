using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Repositories.BaseRepository;

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
}