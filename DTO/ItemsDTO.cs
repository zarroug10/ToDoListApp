using System.Runtime.InteropServices;

namespace ToDoListApp.DTO;

public class ItemsDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public string? UserName { get; set; }
}
