using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Interfaces
{
    /// <summary>
    ///     Интерфейс сервиса пользователя
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     Получение списка пользователей
        /// </summary>
        /// <returns>
        ///     Список пользователей
        /// </returns>
        List<User> GetUsers();

        /// <summary>
        ///     Проверка существования пользователя
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        /// <returns>
        ///     True - пользователь существует,
        ///     False - пользователя не существует
        /// </returns>
        bool IsUserExists(string email);

        /// <summary>
        ///     Добавление пользователя в список
        /// </summary>
        /// <param name="registerData">Данные регистрации</param>
        void Add(RegisterDto registerData);

        /// <summary>
        ///     Поиск пользователя по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>
        ///     User - если пользователь найден, иначе null
        /// </returns>
        User FindById(Guid id);

        /// <summary>
        ///     Поиск пользователя по почте и паролю
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>
        ///     User - если пользователь найден, иначе null
        /// </returns>
        User FindByEmailAndPassword(string email, string password);
    }
}
