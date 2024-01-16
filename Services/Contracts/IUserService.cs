namespace Spotify.Services.Contracts;

public interface IUserService
{
    Task<bool> LikeMusic(int userId, int musicId);
    Task<bool> UnlikeMusic(int userId, int musicId);
}