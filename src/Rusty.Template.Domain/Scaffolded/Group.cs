namespace Rusty.Template.Domain;

public class Group
{
    /// <summary>
    ///     Primary key id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Group name
    /// </summary>
    public string Name { get; set; } = null!;

	public DateTime CreateDate { get; set; }

    /// <summary>
    ///     One to many navigation for User table
    /// </summary>
    public virtual ICollection<User> Users { get; } = new HashSet<User>();
}