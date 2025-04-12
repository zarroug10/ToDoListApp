namespace ToDoListApp.DTO;

public class RegistrationRequest
{
    public int? Age { get; set; }
    public string? Cin { get; set; }
    public string? Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
  
}
