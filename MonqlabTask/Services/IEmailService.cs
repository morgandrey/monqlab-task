namespace MonqlabTask.Services;

public interface IEmailService
{
    void Send(IEnumerable<string> to, string subject, string body, string? from = null);
}