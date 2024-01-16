#nullable disable
using Spotify.Models;
using Spotify.Utils;
using Microsoft.EntityFrameworkCore;
using Spotify.Data.Repositories.Contracts;
namespace Spotify.Data.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly SpotifyContext _context;

    public UserRepository(SpotifyContext context) : base(context)
    {
        _context = context;        
    }

    public async Task<bool> ActiveUser(string token)
    {
        User user = await _context.Users.SingleOrDefaultAsync(e => e.VerificationToken == token);
        if (user == null || user.IsVerified) return false;
        user.IsVerified = true;
        user.VerificationToken = Tools.TokenGenerator();
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User> GetUserForLogin(string email, string password)
    {
        User user = await _context.Users.FirstOrDefaultAsync(e => e.Email == email);

        if (user != null)
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                return user;

        return null;
    }

    public async Task<List<User>> GetUsersWithDetails()
    {
        List<User> users = await _context.Users.Include(e => e.Musics).ToListAsync();
        return users;
    }

    public async Task<User> GetUserWithDetails(int id)
    {
        User user = await _context.Users.Include(e => e.Musics).FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<bool> IsExistedUserByEmail(string email)
    {
        User user = await _context.Users.FirstOrDefaultAsync(e => e.Email == email);
        return user != null;
    }

    public async Task<bool> IsExistedUserByUsername(string username)
    {
        User user = await _context.Users.FirstOrDefaultAsync(e => e.Username == username);
        return user != null;
    }
}