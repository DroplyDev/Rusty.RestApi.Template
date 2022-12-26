using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace prefix.Domain.Scaffolded;

[Keyless]
[Table("UserInfo")]
public class UserInfo
{
    public int UserId { get; set; }

    [StringLength(32)] [Unicode(false)] public string FirstName { get; set; } = null!;

    [StringLength(32)] [Unicode(false)] public string? LastName { get; set; }

    public int Id { get; set; }
}