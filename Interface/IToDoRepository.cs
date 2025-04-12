using ToDoListApp.DTO;
using ToDoListApp.Models;

namespace ToDoListApp.Interface;

public interface IToDoRepository
{

    Task<ToDoItems> GetItemById(string Id);

    Task<IEnumerable<ItemsDTO>> GetItemsByUserId(string UserId);

    Task AddItem(CreateItemDTO item);

    void UpdateItem(ToDoItems item);

    Task DeleteItem(string Id);
    Task<string> Reminder();
    Task<bool> Save();

}
