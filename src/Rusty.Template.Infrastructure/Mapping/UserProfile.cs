#region

using Mapster;
using Rusty.Template.Contracts.Dtos.User;
using Rusty.Template.Domain;

#endregion

namespace Rusty.Template.Infrastructure.Mapping;

public sealed class UserProfile : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<User, UserDto>().TwoWays();
	}
}