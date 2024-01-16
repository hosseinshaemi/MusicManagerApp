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
            UserName = user.Username,
            Email = user.Email,
        };

        return View(profileDTO);
    }


    [HttpPost]
    public async Task<IActionResult> EditProfile(ProfileDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userRepository.GetUserWithDetails(model.id);

            if (user == null)
            {
                return NotFound();
            }

            // Update the user's profile information
            user.Username = model.UserName;
            user.Email = model.Email;
            
            var profileDTO = new ProfileDto {
                UserName = user.Username,
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

}