using Microsoft.AspNetCore.Mvc;
using RestApiApp.Models.DTOs;
using RestApiApp.Services;

namespace RestApiApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all users");
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching user with ID: {Id}", id);
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound(new { message = $"User with ID {id} not found" });
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
    {
        _logger.LogInformation("Creating new user with email: {Email}", dto.Email);
        var user = await _userService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<UserDto>> Update(int id, [FromBody] UpdateUserDto dto)
    {
        _logger.LogInformation("Updating user with ID: {Id}", id);
        var user = await _userService.UpdateAsync(id, dto);
        if (user == null)
            return NotFound(new { message = $"User with ID {id} not found" });
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting user with ID: {Id}", id);
        var deleted = await _userService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"User with ID {id} not found" });
        return NoContent();
    }
}
