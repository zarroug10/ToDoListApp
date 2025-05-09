﻿namespace ToDoListApp.DTO;

public class CreateItemDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }
    public Guid AppUserId { get; set; }
}
