#nullable disable
using System.ComponentModel.DataAnnotations;
namespace Spotify.Models.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "The email is required.")]
    [EmailAddress(ErrorMessage = "This format is not valid for email address.")]
    public string Email { get; set; }
    public string Password { get; set; }
}