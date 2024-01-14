#nullable disable
using System.Net;
using System.Net.Mail;
using Spotify.Utils.Contracts;
using Spotify.Utils.Models;
namespace Spotify.Utils;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(Email email)
    {
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient(_configuration.GetSection("EmailSettings").GetSection("Server").Value);
        message.From = new MailAddress(_configuration.GetSection("EmailSettings").GetSection("From").Value, "SpotifyApp");
        message.To.Add(email.To);
        message.Subject = email.Subject;
        message.Body = email.Body;
        message.IsBodyHtml = true;

        smtp.Port = 587;
        smtp.Credentials = new NetworkCredential(
            _configuration.GetSection("EmailSettings").GetSection("Sender").Value,
            _configuration.GetSection("EmailSettings").GetSection("Credential").Value
        );
        smtp.EnableSsl = true;
        smtp.Send(message);
    }
}