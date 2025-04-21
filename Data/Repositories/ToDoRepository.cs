using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.DTO;
using ToDoListApp.DTO.Enum;
using ToDoListApp.Interface;
using ToDoListApp.Models;

namespace ToDoListApp.Data.Repositories;

public class ToDoRepository(DataContext dataContext, IMapper mapper,IRepository<ToDoItems> repository) : IToDoRepository
{
    public async Task<ToDoItems> GetItemById(string Id)
    { 
        return await repository.GetByIdAsync(Id);
    }

    public async Task<IEnumerable<ItemsDTO>> GetItemsByUserId(string UserId) 
    {
    return repository.GetAllAsync().Result
        .Where(x => x.AppUserId.ToString() == UserId)
        .ProjectTo<ItemsDTO>(mapper.ConfigurationProvider)
        .ToList();
    }

    public async Task  UpdateItem(ToDoItems item)
    {
      await repository.Updatey(item);
    }

    public async Task AddItem(CreateItemDTO item)
    {
        var ToDO = mapper.Map<ToDoItems>(item);
         await repository.AddAsync(ToDO);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteItem(string Id)
    {
        await repository.Delete(Id);
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
