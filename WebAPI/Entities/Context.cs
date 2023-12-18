namespace WebAPI.Entities;

public class Context
{
    /// <summary>
    /// Список пользователей
    /// </summary>
    public List<User> Users { get; set; }

    public Context()
    {
        Users = new List<User>();
        UsersSeed();
    }

    private void UsersSeed()
    {
        Users.Add(new User()
        {
            Id = Guid.Parse("df794708-1a8e-4dd7-b0b7-989de8e91f94"),
            Email = "test1@gmail.com",
            Password = "Alpha1#",
            FirstName = "Alpha",
            LastName = "Beta",
            Patronymic = "Gamma",
            Address = new Address()
            {
                Locality = "Кострома",
                Street = "ул. Советская",
                House = "28"
            }
        });

        Users.Add(new User()
        {
            Id = Guid.Parse("cbb29f2d-621b-4215-9020-192b4d8485f0"),
            Email = "test2@mail.ru",
            Password = "Delta2#",
            FirstName = "Delta",
            LastName = "Sigma",
            Patronymic = "Omega",
            Address = new Address()
            {
                Locality = "Иваново",
                Street = "ул. 8 марта",
                House = "32"
            }
        });

        Users.Add(new User()
        {
            Id = Guid.Parse("9c2f5906-c65a-4b40-be42-a678838c27d5"),
            Email = "test3@yandex.ru",
            Password = "Lambda3#",
            FirstName = "Lambda",
            LastName = "Zeta",
            Patronymic = "Omicron",
            Address = new Address()
            {
                Locality = "Ярославль",
                Street = "ул. Володарского",
                House = "10A"
            }
        });
    }
}
