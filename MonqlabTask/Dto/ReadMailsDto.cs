namespace MonqlabTask.Dto;

public class ReadMailsDto
{
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public IEnumerable<string> Recipients { get; set; } = null!;
    public string MailResult { get; set; } = null!;
    public string? MailFailedMessage { get; set; }
    public DateTime MailDate { get; set; }
}