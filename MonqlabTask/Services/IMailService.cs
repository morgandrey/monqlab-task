namespace MonqlabTask.Services;

public interface IMailService
{
    Task Send(IEnumerable<string> to, string subject, string body, string? from = null);
}