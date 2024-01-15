#nullable disable
namespace Spotify.Models;

public class Music
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Link { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; }
    public ICollection<UserMusic> UserMusics { get; set; }
    public ICollection<User> Users { get; set; }
}