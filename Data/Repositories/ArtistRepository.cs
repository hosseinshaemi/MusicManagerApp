#nullable disable
using Spotify.Models;
using Microsoft.EntityFrameworkCore;
namespace Spotify.Data.Repositories.Contracts;

public class ArtistRepository : GenericRepository<Artist>, IArtistRepository
{
    private readonly SpotifyContext _context;

    public ArtistRepository(SpotifyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Artist> GetArtistByName(string name)
    {
        Artist artist = await _context.Artists.FirstOrDefaultAsync(a => a.Name == name);
        return artist;
    }

    public async Task<List<Artist>> GetArtistsWithDetail()
    {
        List<Artist> artists = await _context.Artists.Include(e => e.Musics).ToListAsync();
        return artists;
    }

    public async Task<Artist> GetArtistWithDetail(int id)
    {
        Artist artist = await _context.Artists.Include(e => e.Musics).FirstOrDefaultAsync(a => a.Id == id);
        return artist;
    }

    public async Task<bool> IsExistedArtistByName(string name)
    {
        Artist artist = await _context.Artists.FirstOrDefaultAsync(a => a.Name == name);
        return artist != null;
    }
}