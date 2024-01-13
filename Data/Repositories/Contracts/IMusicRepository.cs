using Spotify.Models;
namespace Spotify.Data.Repositories.Contracts;

public interface IMusicRepository : IGenericRepository<Music>
{
    Task<List<Music>> GetMusicsWithDetail();
    Task<Music> GetMusicWithDetail(int id);
}