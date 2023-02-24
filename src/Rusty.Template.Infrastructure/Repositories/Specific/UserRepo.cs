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


	public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default, Func<IQueryable<User>, IQueryable<User>>? includes = null)
	{
		return await IncludeIfNotNull(includes)
			.FirstOrDefaultAsync(item => item.UserName == username, cancellationToken);
	}
}