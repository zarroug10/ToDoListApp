using System.Runtime.CompilerServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.DTO;
using ToDoListApp.Interface;
using ToDoListApp.Models;

namespace ToDoListApp.Data.Repositories;

public class UserRepository(DataContext dataContext, IMapper mapper) : IUserRepository
{
    public async Task<IEnumerable<UserDTO>> GetALlUsers(string Search)
    {
        var Filteredusers = await dataContext.Users.Where(x=> x.UserName.Contains(Search) 
                                                      || x.Email.Contains(Search)
                                                      || x.Age.ToString().Contains(Search)
                                                      || x.Cin.ToString().Contains(Search))
                                                      .Include(x=>x.Items)
                                                      .AsNoTracking()
                                     .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                                     .ToListAsync();

        var users = await dataContext.Users.AsNoTracking()
                                           .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                                           .ToListAsync();
        return Search == null ? users: Filteredusers;
    }

    public async Task<AppUser> GetUserById(string UserId)
    {
        var user = await dataContext.Users
                                    .AsNoTracking()
                                    .Where(i => i.Id.ToString() == UserId)
                                    .Include(x=> x.Items)
                                    .FirstOrDefaultAsync();
        return user;
    }

    public Task<UserDTO> GetUserByUSerName(string UserName)
    {
        var user = dataContext.Users
                              .Where(x => x.UserName.ToLower() == UserName.ToLower())
                              .ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync();
        return user;
    }

    public async Task<bool> Save()
    {
        var saved = await dataContext.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<bool> UserExists(string UserName)
    {
        return await dataContext.Users.AnyAsync(x => x.UserName.ToLower() == UserName.ToLower());
    }

    //private IQueryable<TEntity> FilterEntities<TEntity>(DbSet<TEntity> dbSet, string search) where TEntity : class
    //{
    //    var propertyInfo = typeof(TEntity).GetProperties();
    //    IQueryable<TEntity> result = null;

    //    if (propertyInfo == null)
    //    {
    //        throw new ArgumentException($"Properties not found on entity '{typeof(TEntity).Name}'.");
    //    }
    //    foreach (var property in propertyInfo)
    //    {
    //        if (!string.IsNullOrEmpty(search))
    //        {
    //            result = dbSet.AsEnumerable().Where(entity =>
    //            {
    //                var propertyValue = property.GetValue(entity)?.ToString().ToLower();
    //                return propertyValue != null && propertyValue.Contains(search.ToLower());
    //            }).AsQueryable();
    //        }
    //        else
    //        {
    //            return dbSet;
    //        }
    //    }
    //    return result;
    //}
}
