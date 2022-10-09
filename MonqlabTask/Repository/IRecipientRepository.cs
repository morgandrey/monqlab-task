using MonqlabTask.Models;

namespace MonqlabTask.Repository;

public interface IRecipientRepository
{
    Task<Recipient?> GetByEmail(string email);
}