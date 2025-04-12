using ToDoListApp.Models;

namespace ToDoListApp.DTO;

public class UserDTO
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public int? Age { get; set; }
    public string? Cin { get; set; }
    public ICollection<ItemsDTO?> Items { get; set; } = [];
}
