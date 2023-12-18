namespace WebAPI.DTOs;

/// <summary>
///     DTO для аутентификации
/// </summary>
public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
