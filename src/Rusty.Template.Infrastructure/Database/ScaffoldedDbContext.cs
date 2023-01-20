using Microsoft.EntityFrameworkCore;
using Rusty.Template.Domain;

namespace Rusty.Template.Infrastructure.Database;

public partial class ScaffoldedDbContext : DbContext
{
    public ScaffoldedDbContext(DbContextOptions<ScaffoldedDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; } = null!;

    public virtual DbSet<Role> Roles { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Groups_pk");

            entity.HasIndex(e => e.Name, "Groups_Name_uindex").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_Id_uindex");

            entity.HasIndex(e => e.Name, "Roles_Name_uindex").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .IsUnicode(false);

            entity.HasMany(d => d.Users).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("User_Role_Users_Id_fk"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("User_Role_Roles_Id_fk"),
                    j =>
                    {
                        j.HasKey("RoleId", "UserId").HasName("User_Role_pk");
                        j.ToTable("User_Role");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pk");

            entity.HasIndex(e => e.Email, "Users_Email_uindex").IsUnique();

            entity.HasIndex(e => e.UserName, "Users_Username_uindex").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasComment("");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(32)
                .IsUnicode(false);

            entity.HasOne(d => d.Group).WithMany(p => p.Users)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("Users_Groups_Id_fk");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("UserInfo_pk");

            entity.ToTable("UserInfo");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(32)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(32)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithOne(p => p.UserInfo)
                .HasForeignKey<UserInfo>(d => d.UserId)
                .HasConstraintName("UserInfo_Users_Id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
