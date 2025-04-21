using ToDoListApp.DTO;
using ToDoListApp.Models;

namespace ToDoListApp.Interface;

public interface IUserRepository
{
    Task<IEnumerable<AppUser>> GetALlUsers(string Search);

    Task<AppUser> GetUserById(string UserId);

    Task<UserDTO> GetUserByUSerName(string UserName);
    Task<bool> Save();
    Task<bool> UserExists(string UserName);
}
