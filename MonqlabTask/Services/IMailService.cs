using MonqlabTask.Models;

namespace MonqlabTask.Services;

public interface IMailService
{
    /// <summary>
    /// Send the mail.
    /// </summary>
    /// <param name="to">To.</param>
    /// <param name="subject">The mail subject.</param>
    /// <param name="body">The mail body.</param>
    /// <param name="from">From.</param>
    Task SendMail(IEnumerable<string> to, string subject, string body, string? from = null);

    /// <summary>
    /// SaveMailsInDb the mail to database.
    /// </summary>
    /// <param name="mail">The mail.</param>
    /// <param name="recipientEmails">The list of recipient's mails.</param>
    Task SaveMailsInDb(Mail mail, IEnumerable<string> recipientEmails);
}