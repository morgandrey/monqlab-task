using MonqlabTask.Dto;
using MonqlabTask.Models;

namespace MonqlabTask.Repository;

public interface IMailRepository
{
    /// <summary>
    /// Get all mails from database.
    /// </summary>
    /// <returns>The list of mails.</returns>
    Task<IEnumerable<Mail>> GetAll();
    
    /// <summary>
    /// Add the mail to database.
    /// </summary>
    /// <param name="mail">The mail.</param>
    /// <param name="recipientEmails">The list of recipient's mails.</param>
    Task Add(Mail mail, IEnumerable<string> recipientEmails);
}