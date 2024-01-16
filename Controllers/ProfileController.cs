using Spotify.Utils;
using Spotify.Models;
using Spotify.Models.DTOs;
using Spotify.Utils.Models;
using Spotify.Utils.Contracts;
using Microsoft.AspNetCore.Mvc;
using Spotify.Data.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net;
namespace Spotify.Controllers;



public class ProfileController : Controller
{
    private readonly IUserRepository _userRepository;

    public ProfileController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("profile")]
    public async Task<IActionResult> UserProfile(ProfileDto model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        User user = await _userRepository.GetUserWithDetails(int.Parse(userId));
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
    [Route("profile")]
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


}