#nullable disable
using System.ComponentModel.DataAnnotations;

namespace Spotify.Models.DTOs;

public class AddMusicDto
{
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Artist is required.")]
    public string Artist { get; set; }
    
    [Required(ErrorMessage = "Music is required.")]
    public IFormFile Music { get; set; }
}