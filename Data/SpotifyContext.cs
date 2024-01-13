using Microsoft.EntityFrameworkCore;
using Spotify.Models;
namespace Spotify.Data;

public class SpotifyContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Music> Musics { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserMusic> UserMusics { get; set; }

    public SpotifyContext(DbContextOptions<SpotifyContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserMusic>().HasKey(e => new { e.UserId, e.MusicId });
        // modelBuilder.Entity<UserMusic>().HasOne(e => e.User).WithMany(e => e.UserMusics).HasForeignKey(e => e.UserId);
        // modelBuilder.Entity<UserMusic>().HasOne(e => e.Music).WithMany(e => e.UserMusics).HasForeignKey(e => e.MusicId);

        modelBuilder.Entity<User>().HasMany(e => e.Musics).WithMany(e => e.Users).UsingEntity<UserMusic>();

        modelBuilder.Entity<Artist>().HasData(
            new Artist() { Id = 1, Name = "Homayoun Shajarian" },
            new Artist() { Id = 2, Name = "Mohammadreza Shajarian" },
            new Artist() { Id = 3, Name = "Mohammad Motamedi" },
            new Artist() { Id = 4, Name = "Keyvan Kalhor" }
        );

        modelBuilder.Entity<Music>().HasData(
            new Music() { Id = 1, Title = "Abr Biseda Mibarad", FileName = "abr-biseda-mibarad.mp4", ArtistId = 1 },
            new Music() { Id = 2, Title = "Tasnif Ghollab", FileName = "tasnif-ghollab.mp4", ArtistId = 1 },
            new Music() { Id = 3, Title = "Rahe Meykhane", FileName = "rahe-meykhane.mp4", ArtistId = 2 },
            new Music() { Id = 4, Title = "Saghi", FileName = "saghi.mp4", ArtistId = 2 },
            new Music() { Id = 5, Title = "Hala Ke Miravi", FileName = "hala-ke-miravi.mp4", ArtistId = 3 },
            new Music() { Id = 6, Title = "Dastam Ra Begir", FileName = "dastam-ra-begir.mp4", ArtistId = 3 },
            new Music() { Id = 7, Title = "Khatoon", FileName = "khatoon.mp4", ArtistId = 4 },
            new Music() { Id = 8, Title = "Gol Va Khak", FileName = "gol-va-khak.mp4", ArtistId = 4 }
        );

        modelBuilder.Entity<User>().HasData(
            new User() { Id = 1, Email = "hoseinshaemi@gmail.com", IsAdmin = true, Username = "hshaemi", Password = "test123" },
            new User() { Id = 2, Email = "amirhosseinfathi@gmail.com", IsAdmin = false, Username = "afathi", Password = "123test" },
            new User() { Id = 3, Email = "alinikaein@gmail.com", IsAdmin = false, Username = "anikaein", Password = "pass123" },
            new User() { Id = 4, Email = "mammadmmp@gmail.com", IsAdmin = false, Username = "mamadmmp", Password = "123pass" }
        );

        modelBuilder.Entity<UserMusic>().HasData(
            new UserMusic() { UserId = 1, MusicId = 2 },
            new UserMusic() { UserId = 1, MusicId = 3 },
            new UserMusic() { UserId = 2, MusicId = 3 },
            new UserMusic() { UserId = 3, MusicId = 4 },
            new UserMusic() { UserId = 3, MusicId = 5 },
            new UserMusic() { UserId = 4, MusicId = 1 },
            new UserMusic() { UserId = 4, MusicId = 6 }
        );

        base.OnModelCreating(modelBuilder);
    }

}