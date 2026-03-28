using System.ComponentModel.DataAnnotations;

namespace RestApiApp.Models.DTOs;

public class UpdateUserDto
{
    [StringLength(100, MinimumLength = 2)]
    public string? Name { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}
