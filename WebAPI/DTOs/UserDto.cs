using WebAPI.Entities;

namespace WebAPI.DTOs;

/// <summary>
///     DTO пользователя (для смены данных)
/// </summary>
public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public Address Address { get; set; }
}
