using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rusty.Template.Domain;

[Index("GroupName", Name = "index_groupname")]
public class Group
{
    [Key] public int Id { get; set; }

    [StringLength(32)] [Unicode(false)] public string GroupName { get; set; } = null!;

    [InverseProperty("Group")] public virtual ICollection<User> Users { get; } = new List<User>();
}