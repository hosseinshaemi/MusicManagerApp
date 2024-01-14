using Spotify.Models;
using Spotify.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Spotify.Data.Repositories.Contracts;
namespace Spotify.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Index()
    {
        return View("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        if (!ModelState.IsValid) return View(model: login);
        User user = await _userRepository.GetUserForLogin(login.Email, login.Password);
        if (user == null)
        {
            ModelState.AddModelError("Email", "There is no user with this credentials");
            return View(model: login);
        }

        return Content("Hello World");
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        if (!ModelState.IsValid)
            return View(model: register);

        if (await _userRepository.IsExistedUserByEmail(register.Email))
        {
            ModelState.AddModelError("Email", "This email is already registered.");
            return View(model: register);
        }

        if (await _userRepository.IsExistedUserByUsername(register.Username))
        {
            ModelState.AddModelError("Username", "This username is already registered.");
            return View(model: register);
        }

        await _userRepository.Add(new User
        {
            Email = register.Email,
            IsAdmin = false,
            Username = register.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
        });

        return View("SuccessRegister", register);
    }

}
