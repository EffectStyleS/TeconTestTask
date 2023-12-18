using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Infrastructure.Validators;
using WebAPI.Interfaces;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    ///     Получение списка пользователей
    /// </summary>
    /// <returns>Список пользователей</returns>
    [HttpGet]
    public IActionResult GetUsers() => Ok(_userService.GetUsers());

    /// <summary>
    ///     Регистрация пользователя
    /// </summary>
    /// <param name="registerData">Данные регистрации</param>
    /// <returns>
    ///     Ok - успешная регистрация,
    ///     BadRequest - ошибка
    /// </returns>
    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] RegisterDto registerData) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (_userService.IsUserExists(registerData.Email))
        {
            return BadRequest("Пользователь с " + registerData.Email + " уже существует");
        }
            
        _userService.Add(registerData);

        return Ok("Пользователь " + registerData.Email + " успешно зарегистрирован");
    }

    /// <summary>
    ///     Смена пароля
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <param name="changePasswordData">Текущий и новый пароли пользователя</param>
    /// <returns>
    ///     Ok - успешная смена пароля,
    ///     NotFound - пользователь не найден,
    ///     BadRequest - ошибка
    /// </returns>
    [HttpPost]
    [Route("changepassword/{userId}")]
    public IActionResult ChangePassword(Guid userId, [FromBody] ChangePasswordDto changePasswordData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _userService.FindById(userId);
        if (user == null)
        {
            return NotFound("Пользователь не найден");
        }

        if (changePasswordData.CurrentPassword != user.Password)
        {
            return BadRequest("Неверно введён текущий пароль пользователя");
        }

        user.Password = changePasswordData.NewPassword;

        return Ok("Пароль успешно изменён");
    }

    /// <summary>
    ///     Смена данных пользователя
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <param name="userData">Новые данные пользователя</param>
    /// <returns>
    ///     Ok - успешная смена данных пользователя,
    ///     NotFound - пользователь не найден,
    ///     BadRequest - ошибка
    /// </returns> 
    [HttpPatch]
    [Route("{userId}")]
    public IActionResult PatchUser(Guid userId, [FromBody]JsonPatchDocument<UserDto> userData)
    {
        var user = _userService.FindById(userId);
        if (user == null)
        {
            return NotFound("Пользователь не найден");
        }

        var userDto = new UserDto()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Patronymic = user.Patronymic,
            Address = user.Address
        };

        try
        {
            userData.ApplyTo(userDto);
        }
        catch
        {
            return BadRequest("Ошибка данных запроса");
        }

        var userDtoValidator = new UserDtoValidator();
        var validateResult = userDtoValidator.Validate(userDto);
        if (!validateResult.IsValid)
        {
            return BadRequest(validateResult.Errors);
        }

        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Patronymic = userDto.Patronymic;
        user.Address = userDto.Address;

        return Ok(user);
    }

    /// <summary>
    ///     Аутентификация
    /// </summary>
    /// <param name="loginData">Почта и пароль пользователя</param>
    /// <returns>
    ///     Ok - успешный вход,
    ///     BadRequest - ошибка
    /// </returns>
    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginDto loginData)
    {
        var user = _userService.FindByEmailAndPassword(loginData.Email, loginData.Password);

        return user == null ? BadRequest("Неверно указана почта и/или пароль") : Ok(user);
    }
}
