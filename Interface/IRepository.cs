namespace ToDoListApp.Interface;

public interface IRepository<T> where T : class 
{
    Task<IQueryable<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task AddAsync(T entity);
    Task Updatey(T entity);
    Task Delete(string id);
}
