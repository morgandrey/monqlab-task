using MonqlabTask.Models;

namespace MonqlabTask.Repository;

public interface IMailRepository
{
    /// <summary>
    /// Get all mails from database.
    /// </summary>
    /// <returns>The list of mails.</returns>
    Task<IEnumerable<Mail>> GetAll();
}