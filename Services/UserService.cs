using RestApiApp.Models.DTOs;
using RestApiApp.Models.Entities;

namespace RestApiApp.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = new()
    {
        new User { Id = 1, Name = "John Doe", Email = "john@example.com", CreatedAt = DateTime.UtcNow },
        new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", CreatedAt = DateTime.UtcNow }
    };
    private int _nextId = 3;

    public Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = _users.Select(MapToDto);
        return Task.FromResult(users);
    }

    public Task<UserDto?> GetByIdAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user != null ? MapToDto(user) : null);
    }

    public Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Id = _nextId++,
            Name = dto.Name,
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow
        };
        _users.Add(user);
        return Task.FromResult(MapToDto(user));
    }

    public Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return Task.FromResult<UserDto?>(null);

        if (dto.Name != null)
            user.Name = dto.Name;
        if (dto.Email != null)
            user.Email = dto.Email;
        return Task.FromResult<UserDto?>(MapToDto(user));
    }

    public Task<bool> DeleteAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return Task.FromResult(false);

        _users.Remove(user);
        return Task.FromResult(true);
    }

    private static UserDto MapToDto(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        CreatedAt = user.CreatedAt
    };
}
