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
    Task Send(IEnumerable<string> to, string subject, string body, string? from = null);
}