#nullable disable
using System.ComponentModel.DataAnnotations;
namespace Spotify.Models.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "The email is required.")]
    [EmailAddress(ErrorMessage = "This format is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The username is required.")]
    [MaxLength(255, ErrorMessage = "The username must be smaller than 255.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "The password is required.")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one number.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "The confirm password is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
}