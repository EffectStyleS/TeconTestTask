namespace WebAPI.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public Address Address {  get; set; } 
}
