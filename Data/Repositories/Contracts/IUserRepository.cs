using Spotify.Models;
namespace Spotify.Data.Repositories.Contracts;

public interface IUserRepository : IGenericRepository<User>
{
    Task<List<User>> GetUsersWithDetails();
    Task<User> GetUserWithDetails(int id);
}