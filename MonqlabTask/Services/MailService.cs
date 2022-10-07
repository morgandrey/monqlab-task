using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MonqlabTask.Helpers;

namespace MonqlabTask.Services;

public class MailService : IMailService
{
    private readonly AppSettings _appSettings;
    
    public MailService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public async Task Send(IEnumerable<string> to, string subject, string body, string? from = null)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailFrom));
        email.To.AddRange(to.Select(MailboxAddress.Parse).Cast<InternetAddress>().ToList());
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Text) { Text = body };
        
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_appSettings.SmtpUser, _appSettings.SmtpPass);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}