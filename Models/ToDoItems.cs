namespace ToDoListApp.Models;

public class ToDoItems
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
