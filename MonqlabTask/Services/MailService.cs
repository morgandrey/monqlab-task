using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MonqlabTask.DataAccess;
using MonqlabTask.Helpers;
using MonqlabTask.Models;
using MonqlabTask.Repository;

namespace MonqlabTask.Services;

public class MailService : IMailService
{
    private readonly AppSettings _appSettings;
    private readonly EmailDbContext _dbContext;
    private readonly IRecipientRepository _recipientRepository;
    private readonly IMailRepository _mailRepository;
    private readonly ILogger<MailService> _logger;

    public MailService(IOptions<AppSettings> appSettings, IRecipientRepository recipientRepository,
        IMailRepository mailRepository, EmailDbContext dbContext, ILogger<MailService> logger)
    {
        _recipientRepository = recipientRepository;
        _mailRepository = mailRepository;
        _dbContext = dbContext;
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    /// <inheritdoc />
    public async Task SendMail(IEnumerable<string> to, string subject, string body, string? from = null)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
        }
    }

    /// <inheritdoc />
    public async Task SaveMailsInDb(Mail mail, IEnumerable<string> recipientEmails)
    {
        try
        {
            await _dbContext.Database.BeginTransactionAsync();

            await _dbContext.AddAsync(mail);
            await _dbContext.SaveChangesAsync();

            var recipientsList = new List<Recipient>();

            foreach (var recipientEmail in recipientEmails)
            {
                var recipientFromDb = await _recipientRepository.GetByEmail(recipientEmail);
                if (recipientFromDb == null)
                {
                    var newRecipient = new Recipient
                    {
                        RecipientEmail = recipientEmail
                    };

                    await _dbContext.AddAsync(newRecipient);
                    await _dbContext.SaveChangesAsync();
                    recipientsList.Add(newRecipient);
                }
                else
                {
                    recipientsList.Add(recipientFromDb);
                }
            }

            foreach (var recipient in recipientsList)
            {
                await _dbContext.AddAsync(new MailRecipient
                {
                    MailId = mail.MailId,
                    RecipientId = recipient.RecipientId
                });

                await _dbContext.SaveChangesAsync();
            }

            await _dbContext.Database.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("{Error}", ex.Message);
        }
    }
}