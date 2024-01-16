using Spotify.Models;
using Spotify.Services.Contracts;
using Spotify.Data.Repositories.Contracts;
namespace Spotify.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMusicRepository _musicRepository;

    public UserService(IUserRepository userRepository, IMusicRepository musicRepository)
    {
        _userRepository = userRepository;
        _musicRepository = musicRepository;
    }

    public async Task<bool> LikeMusic(int userId, int musicId)
    {
        Music music = await _musicRepository.Get(musicId);
        User user = await _userRepository.GetUserWithDetails(userId);
        
        if (user != null && music != null)
        {
            if (!user.Musics.Any(m => m.Id == music.Id))
            {
                user.Musics.Add(music);
                await _userRepository.Update(user);
                return true;
            }
        }

        return false;
    }

    public async Task<bool> UnlikeMusic(int userId, int musicId)
    {
        Music music = await _musicRepository.Get(musicId);
        User user = await _userRepository.GetUserWithDetails(userId);

        if (user != null && music != null)
        {
            if (user.Musics.Any(m => m.Id == music.Id))
            {
                user.Musics.Remove(music);
                await _userRepository.Update(user);
                return true;
            }
        }

        return false;
    }
}