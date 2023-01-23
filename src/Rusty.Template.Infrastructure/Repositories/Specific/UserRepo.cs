#region

using Rusty.Template.Application.Repositories;
using Rusty.Template.Domain;
using Rusty.Template.Infrastructure.Database;

#endregion

namespace Rusty.Template.Infrastructure.Repositories.Specific;

public sealed class UserRepo : AppDbRepo<User>, IUserRepo
{
	public UserRepo(AppDbContext context) : base(context)
	{
	}


	public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
	{
		return await FirstOrDefaultAsync(item => item.UserName == username, cancellationToken);
	}
}