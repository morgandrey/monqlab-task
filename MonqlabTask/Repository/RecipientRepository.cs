using Microsoft.EntityFrameworkCore;
using MonqlabTask.DataAccess;
using MonqlabTask.Models;

namespace MonqlabTask.Repository;

public class RecipientRepository : IRecipientRepository
{
    private readonly EmailDbContext _dbContext;
    
    public RecipientRepository(EmailDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Recipient?> GetByEmail(string email)
    {
        var recipient = await _dbContext.Recipients.FirstOrDefaultAsync(x => x.RecipientEmail == email);
        return recipient ?? null;
    }
}