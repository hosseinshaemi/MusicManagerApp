namespace Spotify.Services.Contracts;

public interface IAws3Services
{
    Task<string?> UploadFile(IFormFile file);
}