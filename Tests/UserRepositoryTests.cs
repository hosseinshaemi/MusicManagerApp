using Moq;
using Xunit;
using Spotify.Data;
using Spotify.Models;
using Moq.EntityFrameworkCore;
using Spotify.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Spotify.Data.Repositories.Contracts;
namespace Spotify.Tests;

public class UserRepositoryTests
{
    [Fact]
    public async Task GetUserByEmailTest()
    {
        var mockedContext = new Mock<SpotifyContext>();
        mockedContext.Setup<DbSet<User>>(x => x.Users).ReturnsDbSet(
            new List<User>
            {
                new User { Id = 1, Email = "hoseinshaemi@gmail.com", Username = "hshaemi", Password = "Test123", IsVerified = true, IsAdmin = true },
                new User { Id = 2, Email = "f.amirhosssein81@gmail.com", Username = "f.amirhosssein81", Password = "123Test", IsVerified = true, IsAdmin = true },
                new User { Id = 3, Email = "alinikaein@gmail.com", Username = "anikaein", Password = "alinik", IsVerified = true, IsAdmin = true }
            }
        );

        IUserRepository userRepository = new UserRepository(mockedContext.Object);
        var user = await userRepository.GetUserByEmail("hoseinshaemi@gmail.com");
        Assert.NotNull(user);
        Assert.Equal("hshaemi", user.Username);
    }
}