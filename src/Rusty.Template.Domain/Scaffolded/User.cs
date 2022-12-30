using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rusty.Template.Domain;

[Index("UserName", Name = "index_username")]
public class User
{
    [Key] public int Id { get; set; }

    [StringLength(32)] [Unicode(false)] public string UserName { get; set; } = null!;

    [StringLength(32)] [Unicode(false)] public string Password { get; set; } = null!;

    public int? GroupId { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("Users")]
    public virtual Group? Group { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}