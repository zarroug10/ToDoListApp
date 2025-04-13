using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using ToDoListApp.DTO;
using ToDoListApp.Models;

﻿using AutoMapper;
namespace ToDoListApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IMapper mapper , UserManager<AppUser> userManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
    {
        if(registrationRequest == null)
        {
            return BadRequest("Invalid request");
        }
        var user = mapper.Map<AppUser>(registrationRequest);
        var result = await userManager.CreateAsync(user, registrationRequest.Password);

        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }

    [HttpPost]
    public Task<IActionResult> Login()
    {
        //TODO: Implement this method
        throw new NotImplementedException();
    }
}
    