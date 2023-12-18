using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Tests.Controllers;

public class UserControllerTests
{
    /// <summary>
    ///     Тест успешной регистрации
    /// </summary>
    [Fact]
    public void UserController_Register_ReturnOk()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);
        var registerData = new RegisterDto();
        A.CallTo(() => userService.IsUserExists(registerData.Email)).Returns(false);

        var result = userController.Register(registerData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    /// <summary>
    ///     Тест неудачной регистрации
    /// </summary>
    [Fact]
    public void UserController_Register_ReturnBadRequest()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);
        var registerData = new RegisterDto();
        A.CallTo(() => userService.IsUserExists(registerData.Email)).Returns(true);

        var result = userController.Register(registerData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    /// <summary>
    ///     Тест успешной смены пароля
    /// </summary>
    [Fact]
    public void UserController_ChangePassword_ReturnOk()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);

        var userId = Guid.Parse("df794708-1a8e-4dd7-b0b7-989de8e91f94");

        var user = new User
        {
            Password = "Alpha1#"
        };

        var changePasswordData = new ChangePasswordDto
        {
            CurrentPassword = "Alpha1#",
            NewPassword = "TestNewPassword1"
        };

        A.CallTo(() => userService.FindById(userId)).Returns(user);

        var result = userController.ChangePassword(userId, changePasswordData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    /// <summary>
    ///     Тест неудачной смены пароля,
    ///     при неправильном Id пользователя
    /// </summary>
    [Fact]
    public void UserController_ChangePassword_ReturnNotFound()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);

        var userId = Guid.Empty;
        var changePasswordData = new ChangePasswordDto();
        A.CallTo(() => userService.FindById(userId)).Returns(null);

        var result = userController.ChangePassword(userId, changePasswordData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
    }

    /// <summary>
    ///     Тест неудачной смены пароля,
    ///     при неверно заданном текущем пароле
    /// </summary>
    [Fact]
    public void UserController_ChangePassword_ReturnBadRequest()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);

        var userId = Guid.Parse("df794708-1a8e-4dd7-b0b7-989de8e91f94");

        var user = new User
        {
            Password = "Alpha1#"
        };

        var changePasswordData = new ChangePasswordDto
        {
            CurrentPassword = "WrondPass",
            NewPassword = "TestNewPassword1"
        };

        A.CallTo(() => userService.FindById(userId)).Returns(user);

        var result = userController.ChangePassword(userId, changePasswordData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }

    /// <summary>
    ///     Тест удачной смены данных пользователя
    /// </summary>
    [Fact]
    public void UserController_PatchUser_ReturnOk()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);

        var userId = Guid.Parse("df794708-1a8e-4dd7-b0b7-989de8e91f94");
        var user = new User
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Patronymic = "TestPatronymic",
            Address = new Address
            {
                Locality = "TestLocality",
                Street = "TestStreet",
                House = "TestHouse"
            }
        };

        var userData = new JsonPatchDocument<UserDto>();
        userData.Replace(user => user.FirstName, "TestReplace");

        A.CallTo(() => userService.FindById(userId)).Returns(user);

        var result = userController.PatchUser(userId, userData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        result.As<OkObjectResult>().Value.Should().BeAssignableTo<User>();
    }

    /// <summary>
    ///     Тест неудачной смены данных пользователя
    ///     при неверно заданном текущем пароле 
    /// </summary>
    [Fact]
    public void UserController_PatchUser_ReturnNotFound()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);

        var userId = Guid.Empty;
        var userData = new JsonPatchDocument<UserDto>();
        A.CallTo(() => userService.FindById(userId)).Returns(null);

        var result = userController.PatchUser(userId, userData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
    }

    /// <summary>
    ///     Тест удачной аутентификации
    /// </summary>
    [Fact]
    public void UserController_Login_ReturnOk()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);

        var user = new User();
        var loginData = new LoginDto();
        A.CallTo(() => userService.FindByEmailAndPassword(loginData.Email, loginData.Password)).Returns(user);

        var result = userController.Login(loginData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        result.As<OkObjectResult>().Value.Should().BeAssignableTo<User>();
    }

    /// <summary>
    ///     Тест неудачной аутентификации
    ///     при неверно заданной почте или пароле
    /// </summary>
    [Fact]
    public void UserController_Login_ReturnNotFound()
    {
        var userService = A.Fake<IUserService>();
        var userController = new UserController(userService);

        var loginData = new LoginDto();
        A.CallTo(() => userService.FindByEmailAndPassword(loginData.Email, loginData.Password)).Returns(null);

        var result = userController.Login(loginData);

        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
    }
}
