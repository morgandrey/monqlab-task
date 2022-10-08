namespace MonqlabTask.Dto;

public class ReadMailsDto
{
    /// <summary>
    /// The mail subject.
    /// </summary>
    public string Subject { get; set; } = null!;
    
    /// <summary>
    /// The mail body.
    /// </summary>
    public string Body { get; set; } = null!;
    
    /// <summary>
    /// The list of recipient's mails.
    /// </summary>
    public IEnumerable<string> Recipients { get; set; } = null!;
    
    /// <summary>
    /// The mail result.
    /// </summary>
    public string MailResult { get; set; } = null!;
    
    /// <summary>
    /// The failed message of mail.
    /// </summary>
    public string? MailFailedMessage { get; set; }
    
    /// <summary>
    /// The mail date.
    /// </summary>
    public DateTime MailDate { get; set; }
}