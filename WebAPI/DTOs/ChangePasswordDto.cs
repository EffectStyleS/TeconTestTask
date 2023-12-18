namespace WebAPI.DTOs;

/// <summary>
///     DTO для смены пароля
/// </summary>
public class ChangePasswordDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
