using Spotify.Utils;
using Spotify.Models;
using Spotify.Models.DTOs;
using Spotify.Utils.Models;
using Spotify.Utils.Contracts;
using Microsoft.AspNetCore.Mvc;
using Spotify.Data.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Spotify.Controllers;

[AutoValidateAntiforgeryToken]
public class AccountController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailSender _emailSender;
    private readonly IViewRenderService _viewRenderService;

    public AccountController(IUserRepository userRepository, IEmailSender emailSender, IViewRenderService viewRenderService)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
        _viewRenderService = viewRenderService;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Login");
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

        User user = await _userRepository.Add(new User
        {
            Email = register.Email,
            Username = register.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
            VerificationToken = Tools.TokenGenerator()
        });

        SuccessfullRegisterDto srd = new() {
            Username = user.Username,
            VerificationUrl = Url.Action(
                "Validate",
                "Account",
                new { token = user.VerificationToken },
                Url.ActionContext.HttpContext.Request.Scheme,
                Url.ActionContext.HttpContext.Request.Host.Value,
                null
            )
        };

        Email verificationEmail = new()
        {
            To = register.Email,
            Body = await _viewRenderService.RenderToStringAsync("_ActiveEmail", srd),
            Subject = "Activation"
        };
        _emailSender.SendEmail(verificationEmail);

        return View("SuccessRegister", register);
    }

    [HttpGet]
    public async Task<IActionResult> Validate(string token)
    {
        ViewBag.IsActive = await _userRepository.ActiveUser(token);
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

}
