using Spotify.Services.Models;
namespace Spotify.Services.Contracts;

public interface IEmailSender
{
    void SendEmail(Email email);
}