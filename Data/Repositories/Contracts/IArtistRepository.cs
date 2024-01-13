using Spotify.Models;
namespace Spotify.Data.Repositories.Contracts;

public interface IArtistRepository : IGenericRepository<Artist>
{
    Task<List<Artist>> GetArtistsWithDetail();
    Task<Artist> GetArtistWithDetail(int id);
}