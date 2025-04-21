using Microsoft.EntityFrameworkCore;
using ToDoListApp.Interface;
using ToDoListApp.Models;

namespace ToDoListApp.Data.Repositories;

public class Repository<T>(DataContext db) : IRepository<T> where T : notnull, BasEntity
{
    public async Task<IQueryable<T>> GetAllAsync()
    {
         return db.Set<T>().AsQueryable();
    }
    public async Task<T> GetByIdAsync(string id)
    {
        return await db.Set<T>().FirstAsync(i => i.Id.ToString() == id);
    }

    public async Task AddAsync(T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
    }
    public async Task Updatey(T entity)
    {
        db.Entry(entity).State = EntityState.Modified;
        await db.SaveChangesAsync();
    }
    public async Task Delete(string id)
    {
        var entity = await GetByIdAsync(id);
         db.Set<T>().Remove(entity);
         await  db.SaveChangesAsync();
    }
}



