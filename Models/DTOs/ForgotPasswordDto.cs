using System.ComponentModel.DataAnnotations;
namespace Spotify.Models.DTOs;

public class ForgotPasswordDto
{
    [Required(ErrorMessage = "The email is required.")]
    [EmailAddress(ErrorMessage = "The email format is not correct.")]
    public string? Email { get; set; }
}