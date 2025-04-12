using Microsoft.AspNetCore.Identity;
namespace ToDoListApp.Models;

public class AppUser : IdentityUser<Guid>
{
    public int Age { get; set; }
    public string? Cin { get; set; }
    public ICollection<ToDoItems> Items { get;} = [];
    public ICollection<AppUserRole> Roles { get; set; } = [];
}
