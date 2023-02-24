
namespace Rusty.Template.Domain;

public partial class UserInfo
{
    /// <summary>
    ///     User first name
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    ///     User last name
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    ///     User id foreign key
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///     One to one navigation for User table
    /// </summary>
    public virtual User User { get; set; } = null!;
}
