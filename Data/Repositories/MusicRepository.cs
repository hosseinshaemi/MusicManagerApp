#nullable disable
using Spotify.Models;
using Microsoft.EntityFrameworkCore;
using Spotify.Data.Repositories.Contracts;
namespace Spotify.Data.Repositories;

public class MusicRepository : GenericRepository<Music>, IMusicRepository
{
    private readonly SpotifyContext _context;

    public MusicRepository(SpotifyContext context) : base(context)
    {
        _context = context;        
    }

    public async Task<List<Music>> GetMusicsWithDetail()
    {
        List<Music> musics = await _context.Musics.Include(e => e.Users).ToListAsync();
        return musics;
    }

    public async Task<Music> GetMusicWithDetail(int id)
    {
        Music music = await _context.Musics.Include(e => e.Users).FirstOrDefaultAsync(m => m.Id == id);
        return music;
    }
}