namespace Spotify.Utils.Contracts;

public interface IAws3Services
{
    Task<string?> UploadFile(IFormFile file);
}