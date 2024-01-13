#nullable disable
namespace Spotify.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<UserMusic> UserMusics { get; set; }
    public ICollection<Music> Musics { get; set; }
}