using Spotify.Models;
namespace Spotify.Data.Repositories.Contracts;

public interface IUserRepository : IGenericRepository<User>
{
    Task<List<User>> GetUsersWithDetails();
    Task<User> GetUserWithDetails(int id);
    Task<bool> IsExistedUserByEmail(string email);
    Task<bool> IsExistedUserByUsername(string username);
    Task<User> GetUserForLogin(string email, string password);
}