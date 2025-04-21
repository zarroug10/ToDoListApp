using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models;

public abstract class BasEntity
{
    [Key]
    public Guid Id { get; set; }
}
