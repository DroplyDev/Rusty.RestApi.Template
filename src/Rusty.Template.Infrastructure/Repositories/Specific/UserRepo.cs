#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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


	public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
	{
		return await FirstOrDefaultAsync(item => item.UserName == username, cancellationToken);
	}

	public async Task<User?> GetByUsernameAsync(string username, Func<IQueryable<User>,
														IIncludableQueryable<User, object>>?
													includes = null, CancellationToken cancellationToken = default)
	{
		return await IncludeIfNotNull(includes)
			.FirstOrDefaultAsync(item => item.UserName == username, cancellationToken);
	}
}