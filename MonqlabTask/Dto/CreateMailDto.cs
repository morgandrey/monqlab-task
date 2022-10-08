namespace MonqlabTask.Dto;

public class CreateMailDto
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
}