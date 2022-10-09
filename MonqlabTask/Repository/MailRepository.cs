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

    /// <inheritdoc />
    public async Task<IEnumerable<Mail>> GetAll()
    {
        return await _dbContext.Mail
            .Include(x => x.MailRecipients)
            .ThenInclude(x => x.Recipient)
            .ToListAsync();
    }
}