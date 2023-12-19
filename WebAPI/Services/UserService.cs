using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Services;

/// <summary>
///     Реализация сервиса пользователя
/// </summary>
public class UserService : IUserService
{
    private readonly Context _context;

    public UserService(Context context)
    {
        _context = context;
    }

    #region IUserService implementation

    public List<User> GetUsers() => _context.Users;

    public bool IsUserExists(string email) => _context.Users.Any(u => u.Email == email);

    public void Add(RegisterDto registerData)
    {
        _context.Users.Add(new User()
        {
            Id = Guid.NewGuid(),
            Password = registerData.Password,
            Email = registerData.Email,
            FirstName = registerData.FirstName,
            LastName = registerData.LastName,
            Patronymic = registerData.Patronymic,
            Address = registerData.Address
        });
    }

    public User FindById(Guid id) => _context.Users.Find(x => x.Id == id);

    public User FindByEmailAndPassword(string email, string password) => _context.Users.Find(x => x.Email == email && x.Password == password);

    #endregion  
}
