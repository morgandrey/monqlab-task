using Microsoft.EntityFrameworkCore;
using MonqlabTask.DataAccess;
using MonqlabTask.Models;

namespace MonqlabTask.Repository;

public class MailRepository : IMailRepository
{
    private readonly EmailDbContext _dbContext;

    public MailRepository(EmailDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Mail>> GetAll()
    {
        return await _dbContext.Mail
            .Include(x => x.MailRecipients)
            .ThenInclude(x => x.Recipient)
            .ToListAsync();
    }

    public async Task Add(Mail mail, IEnumerable<string> recipientEmails)
    {
        await _dbContext.Database.BeginTransactionAsync();

        await _dbContext.AddAsync(mail);
        await _dbContext.SaveChangesAsync();

        var recipientsList = recipientEmails
            .Select(recipientEmail => new Recipient { RecipientEmail = recipientEmail })
            .ToList();
        
        await _dbContext.AddRangeAsync(recipientsList);
        await _dbContext.SaveChangesAsync();

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
}