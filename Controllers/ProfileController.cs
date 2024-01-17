using Spotify.Models;
using Spotify.Models.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotify.Data.Repositories.Contracts;
using Spotify.Services.Contracts;
namespace Spotify.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public ProfileController(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        User user = await _userRepository.GetUserByEmail(User.Identity!.Name!);
        if (user == null) return NotFound();
        ProfileDto profileDto = new() { Email = user.Email, Username = user.Username };
        return View(profileDto);
    }

    [HttpGet]
    public async Task<IActionResult> UserProfile(ProfileDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        User user = await _userRepository.GetUserWithDetails(int.Parse(userId!));
        if (user == null)
        {
            return NotFound();
        }
        var profileDTO = new ProfileDto
        {
            Username = user.Username,
            Email = user.Email,
        };

        return View(profileDTO);
    }


    [HttpPost]
    public async Task<IActionResult> EditProfile(ProfileDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userRepository.GetUserByEmail(model.Email!);

            if (user == null)
            {
                return NotFound();
            }

            // Update the user's profile information
            user.Username = model.Username;
            user.Email = model.Email;
            
            var profileDTO = new ProfileDto {
                Username = user.Username,
                Email = user.Email,
            };

            return View(profileDTO);
        }

        // If ModelState is not valid, return to the edit form with validation errors
        return View("EditProfile", model);
    }

    [HttpGet]
    public async Task<IActionResult> Like(int id)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _userService.LikeMusic(userId, id);
        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Unlike(int id)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _userService.UnlikeMusic(userId, id);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePassword)
    {
        if (!ModelState.IsValid) return View(changePassword);

        User user = await _userRepository.GetUserByEmail(User.Identity!.Name!);
        if (user == null) return NotFound();

        if (!BCrypt.Net.BCrypt.Verify(changePassword.LastPassword, user.Password))
        {
            ModelState.AddModelError("LastPassword", "The last password is not correct.");
            return View(changePassword);
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
        await _userRepository.Update(user);

        ViewBag.IsSuccess = true;
        return View();
    }

}