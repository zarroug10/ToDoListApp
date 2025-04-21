using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.DTO;
using ToDoListApp.DTO.Enum;
using ToDoListApp.Interface;
using ToDoListApp.Models;

namespace ToDoListApp.Data.Repositories;

public class ToDoRepository(DataContext dataContext, IMapper mapper) : IToDoRepository
{
    public async Task<ToDoItems> GetItemById(string Id)
    {
        IQueryable<ToDoItems> item = dataContext.ToDoItems
                                    .AsNoTracking()
                                    .Where(i => i.Id.ToString() == Id);
        return await item.FirstOrDefaultAsync(); 
    }

    public async Task<IEnumerable<ItemsDTO>> GetItemsByUserId(string UserId) 
    {
        var item = await dataContext.ToDoItems
                                    .AsNoTracking()
                                    .Where(i => i.AppUserId.ToString() == UserId)
                                    .ProjectTo<ItemsDTO>(mapper.ConfigurationProvider)
                                    .ToListAsync();
        return item;
    }

    public void  UpdateItem(ToDoItems item)
    {
       dataContext.Entry(item).State = EntityState.Modified;
    }

    public async Task AddItem(CreateItemDTO item)
    {
        var ToDO = mapper.Map<ToDoItems>(item);
         await dataContext.ToDoItems.AddAsync(ToDO);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteItem(string Id)
    {
        var item = dataContext.ToDoItems.Find(Id);
        dataContext.ToDoItems.Remove(dataContext.ToDoItems.Find(Id));
        await dataContext.SaveChangesAsync();
    }

    public async Task<string> Reminder()
    {
        string message = "";
        var itemsDeadlines = await dataContext.ToDoItems.OrderBy(x => x.Id)
                                               .Select(x => x.Deadline)
                                               .ToListAsync();
        foreach (var item in itemsDeadlines)
        {
            TimeSpan timeLeft = item - DateTime.Now;
            switch (timeLeft.Days)
            {
                case >= 5 and < 7:
                    message = "You have less than a week to complete this task";
                    break;
                case >=2 and <4:
                    message = "You have less than 4 days to complete this task";
                    break;
                case 1:
                    message = "You have less than a day to complete this task";
                    break;
                default:
                    message = "";
                    break;
            }
        }
        return message;
    }

    public async Task<bool> Save()
    {
        var saved = await dataContext.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<ItemsDTO> StatusUpdate(string id)
    {
        var item = await dataContext.ToDoItems.FirstOrDefaultAsync(i => i.Id.ToString() == id);

        if (item == null) return null;

        if (item.Deadline < DateTime.Now && item.IsCompleted == false)
        {
            item.Status = Status.Expired;
        }
        else if (item.Deadline > DateTime.Now)
        {
            item.Status = Status.InProgress;
        }
        else // Extremely rare case
        {
            item.Status = Status.Completed;
        }

        // Save changes to the DB
        await dataContext.SaveChangesAsync();

        // Map to DTO after update
        return mapper.Map<ItemsDTO>(item);
    }
}
