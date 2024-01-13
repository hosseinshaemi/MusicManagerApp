using Spotify.Models;
using Microsoft.AspNetCore.Mvc;
using Spotify.Data.Repositories.Contracts;
using System.Text.Json;
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
        return View();
    }

    public async Task<IActionResult> GetUser(int id)
    {
        User user = await _userRepository.GetUserWithDetails(id);
        Console.WriteLine(user.Email + " " + user.Username);
        return Ok("Hello World");
    }

}
