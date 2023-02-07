
namespace Rusty.Template.Domain;

public partial class Role
{
    /// <summary>
    ///     Primary key id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Role name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Many to many navigation for User table
    /// </summary>
    public virtual ICollection<User> Users { get; } = new HashSet<User>();
}
