#nullable disable
namespace Spotify.Models.DTOs;

public class SuccessfullRegisterDto
{
    public string Username { get; set; }
    public string VerificationUrl { get; set; }
}