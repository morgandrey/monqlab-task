namespace MonqlabTask.Dto;

public class CreateMailDto
{
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public IEnumerable<string> Recipients { get; set; } = null!;
}