using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MonqlabTask.Helpers;

namespace MonqlabTask.Services;

public class EmailService : IEmailService
{
    private readonly AppSettings _appSettings;
    
    public EmailService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public void Send(IEnumerable<string> to, string subject, string body, string? from = null)
    {
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailFrom));
        email.To.AddRange(to.Select(MailboxAddress.Parse).Cast<InternetAddress>().ToList());
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Text) { Text = body };
        
        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, SecureSocketOptions.StartTls);
        smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}