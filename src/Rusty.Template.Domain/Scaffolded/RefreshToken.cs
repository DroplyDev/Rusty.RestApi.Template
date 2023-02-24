using System;

namespace Rusty.Template.Domain;

public partial class RefreshToken
{
    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsUsed { get; set; }

    public bool IsInvalidated { get; set; }

    /// <summary>
    ///     One to one navigation for User table
    /// </summary>
    public virtual User User { get; set; } = null!;
}
