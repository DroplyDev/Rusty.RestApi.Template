using Microsoft.EntityFrameworkCore;
using Rusty.Template.Domain;

#pragma warning disable CS1591

namespace Rusty.Template.Infrastructure.Database;

public partial class ScaffoldedDbContext : DbContext
{
    public ScaffoldedDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity => { entity.HasKey(e => e.Id).HasName("PK__Groups__3214EC07A5CCA748"); });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07D7D8A3BE");

            entity.HasMany(d => d.Users).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__User_Role__UserI__2F10007B"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__User_Role__RoleI__2E1BDC42"),
                    j =>
                    {
                        j.HasKey("RoleId", "UserId").HasName("PK__User_Rol__5B8242DEBDCF1821");
                        j.ToTable("User_Role");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0717E6E9A2");

            entity.HasOne(d => d.Group).WithMany(p => p.Users).HasConstraintName("FK__Users__GroupId__276EDEB3");
        });

        modelBuilder.Entity<UserInfo>(entity => { entity.Property(e => e.Id).ValueGeneratedOnAdd(); });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}