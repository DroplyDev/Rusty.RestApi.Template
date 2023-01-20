namespace Rusty.Template.Domain;

public class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? GroupId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Group? Group { get; set; }

    public virtual UserInfo? UserInfo { get; set; }

    public virtual ICollection<Role> Roles { get; } = new HashSet<Role>();
}
