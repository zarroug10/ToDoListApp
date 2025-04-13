using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDoListApp.Data;
using ToDoListApp.DTO;
using ToDoListApp.Interface;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository,IMapper mapper, DataContext dataContext) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetUsers(string? Search)
    {
        try 
        { 
        var users = await userRepository.GetALlUsers(Search);
        if(users == null)
        {
            return NotFound();
        }
        return Ok(users);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("user/{UserId}")]
    public async Task<IActionResult> GetUserById(string? UserId)
    {
        var user = await userRepository.GetUserById(UserId);
        if (user == null) return NotFound("User is not found");
        var result = mapper.Map<UserDTO>(user);
        return Ok(result);
    }

    [HttpGet]
    [Route("{UserName}")]
    public async Task<IActionResult> GetUserByUserName(string UserName)
    {
        var user = await userRepository.GetUserByUSerName(UserName);
        if(user == null) return NotFound("User is not found");
        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(string? userId , UpdateRequest updateRequest)
    {
       try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return BadRequest(errors);
            }

            var user = await userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(updateRequest.UserName) || string.IsNullOrEmpty(updateRequest.Email))
            {
                return BadRequest("null or empty Fields are not allowed !");
            }
            if (await userRepository.UserExists(updateRequest.UserName))
            {
                return BadRequest("this Username is Already in Use");
            }

            mapper.Map(updateRequest, user);

            dataContext.Entry(user).State = EntityState.Modified;

            if (!await userRepository.Save())
            {
                return BadRequest("Failed to update user");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
