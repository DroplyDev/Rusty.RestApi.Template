using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prefix.Domain.Scaffolded;

[Index("Name", Name = "index_username")]
public class Role
{
    [Key] public int Id { get; set; }

    [StringLength(32)] [Unicode(false)] public string Name { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<User> Users { get; } = new List<User>();
}