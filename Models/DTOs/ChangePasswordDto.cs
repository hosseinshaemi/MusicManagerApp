#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Spotify.Models.DTOs;

public class ChangePasswordDto
{
    [Required(ErrorMessage = "You should enter the last password.")]
    [DataType(DataType.Password)]
    [DisplayName("Last Password")]
    public string LastPassword { get; set; }

    [Required(ErrorMessage = "You should enter the new password.")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one number.")]
    [DisplayName("New Password")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "You should enter the confirm password.")]
    [DataType(DataType.Password)]
    [DisplayName("Confirm New Password")]
    [Compare("NewPassword")]
    public string ConfirmNewPassword { get; set; }
}