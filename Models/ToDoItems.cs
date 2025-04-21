using ToDoListApp.DTO.Enum;

namespace ToDoListApp.Models;

public class ToDoItems:BasEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public Status Status { get; set; } = Status.InProgress;
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
