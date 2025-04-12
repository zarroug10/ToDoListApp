using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Models;

namespace ToDoListApp.Data;

public class DataContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
{
    public DbSet<ToDoItems> ToDoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
       .HasMany(r => r.Roles)
       .WithOne(u => u.User)
       .HasForeignKey(k => k.UserId)
       .IsRequired();

        builder.Entity<AppRole>() 
        .HasMany(r => r.UserRoles) 
        .WithOne(u => u.Role) 
        .HasForeignKey(k => k.RoleId)
        .IsRequired();

        builder.Entity<ToDoItems>()
            .HasOne(e=> e.AppUser)
            .WithMany(l=> l.Items)
            .HasForeignKey(e=> e.AppUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
