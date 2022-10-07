using Microsoft.EntityFrameworkCore;
using MonqlabTask.DataAccess;
using MonqlabTask.Models;

namespace MonqlabTask.Repository;

public class MailRepository : IRepository
{
    private readonly EmailDbContext _dbContext;
    
    public MailRepository(EmailDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Mail>> GetAllMessages()
    {
        return await _dbContext.Mail.ToListAsync();
    }

    public Task AddNewEmail(Mail mail)
    {
        throw new NotImplementedException();
    }
}