#nullable disable
namespace Spotify.Models;

public class UserMusic
{
    public int MusicId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public Music Music { get; set; }
}