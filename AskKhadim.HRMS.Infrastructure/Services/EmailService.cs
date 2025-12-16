using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body, string? replyTo = null);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(
        string to,
        string subject,
        string body,
        string? replyTo = null)
    {
        var message = new MimeMessage();

        message.From.Add(
            MailboxAddress.Parse(_config["Email:Smtp:From"]) 
        );

        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        if (!string.IsNullOrWhiteSpace(replyTo))
        {
            message.ReplyTo.Add(MailboxAddress.Parse(replyTo));
        }

        message.Body = new TextPart("html")
        {
            Text = body
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        await smtp.ConnectAsync(
            _config["Email:Smtp:Host"],
            int.Parse(_config["Email:Smtp:Port"]),
            SecureSocketOptions.Auto
        );

        smtp.AuthenticationMechanisms.Remove("XOAUTH2");

        await smtp.AuthenticateAsync(
            _config["Email:Smtp:Username"],
            _config["Email:Smtp:Password"]
        );

        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}
