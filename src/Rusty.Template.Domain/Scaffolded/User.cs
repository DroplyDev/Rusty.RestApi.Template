using System;

namespace Rusty.Template.Domain;

public partial class User
{
    /// <summary>
    ///     Primary key id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Unique UserName for login and identification
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    ///     User strong password
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    ///     User email
    /// </summary>
    public string Email { get; set; } = null!;

    public string? RefreshToken { get; set; }

    /// <summary>
    ///     Group id foreign key
    /// </summary>
    public int? GroupId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    /// <summary>
    ///     Prop that shows if user is deleted
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    ///     Many to one navigation for Group table
    /// </summary>
    public virtual Group? Group { get; set; }

    /// <summary>
    ///     One to one navigation for UserInfo table
    /// </summary>
    public virtual UserInfo? UserInfo { get; set; }

    /// <summary>
    ///     Many to many navigation for Role table
    /// </summary>
    public virtual ICollection<Role> Roles { get; } = new HashSet<Role>();
}
