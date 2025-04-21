using System.Runtime.InteropServices;
using ToDoListApp.DTO.Enum;

namespace ToDoListApp.DTO;

public class ItemsDTO
{
    public Status Status { get; set; } = Status.InProgress;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public string? UserName { get; set; }
}
