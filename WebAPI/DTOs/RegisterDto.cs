using WebAPI.Entities;

namespace WebAPI.DTOs;

/// <summary>
///     DTO для регистрации
/// </summary>
public class RegisterDto
{
    public string Email {  get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public Address Address { get; set; }
}
