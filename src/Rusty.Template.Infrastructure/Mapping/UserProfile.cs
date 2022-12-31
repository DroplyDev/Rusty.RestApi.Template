using Mapster;
using Rusty.Template.Contracts.Dtos.User;
using Rusty.Template.Domain;

namespace Rusty.Template.Infrastructure.Mapping;

/// <summary>
///     The weather forecast profile class
/// </summary>
/// <seealso cref="IRegister" />
public sealed class UserProfile : IRegister
{
    /// <summary>
    ///     Registers the config
    /// </summary>
    /// <param name="config">The config</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>().TwoWays();
    }
}