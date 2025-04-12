using Microsoft.AspNetCore.Identity;

namespace ToDoListApp.Models;

public class AppRole:IdentityRole<Guid>
{
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
}
