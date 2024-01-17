#nullable disable
namespace Spotify.Models.DTOs;

public class ForgotPasswordEmailDto
{
    public string Username { get; set; }
    public string ResetLink { get; set; }
}