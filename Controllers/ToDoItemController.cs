using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.DTO;
using ToDoListApp.Interface;

namespace ToDoListApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ToDoItemController(IToDoRepository toDoRepository, IMapper mapper) : Controller
{

    [HttpGet("user/{UserId}")]
    public async Task<IActionResult> GetItemsByUserId(string UserId)
    {
        var items = await toDoRepository.GetItemsByUserId(UserId);
        if (items == null) return BadRequest("Invalid user or userId");
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem(CreateItemDTO itemsDTO)
    {
        await toDoRepository.AddItem(itemsDTO);
        return Ok("Item added successfully");
    }

    [HttpPut("item/{itemId?}")]
    public async Task<IActionResult> UpdateItem(string? itemId, UpdateItemRequest updateItem)
    {
        var item = await toDoRepository.GetItemById(itemId);

        if (item == null) return NotFound("Item is not found");

        mapper.Map(updateItem, item);

        toDoRepository.UpdateItem(item);

        if (!await toDoRepository.Save()) return BadRequest("Error While Saving");

        return NoContent();
    }

    [HttpDelete("item/delete/{itemId?}")]
    public async Task<IActionResult> DeleteAsync(string? itemId)
    {
        await toDoRepository.DeleteItem(itemId);
        return NoContent();
    }
}
    