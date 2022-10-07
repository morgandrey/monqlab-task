namespace MonqlabTask.Helpers;

public class AppSettings
{
    public int RefreshTokenTTL { get; set; }
    public string EmailFrom { get; set; } = null!;
    public string SmtpHost { get; set; } = null!;
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; } = null!;
    public string SmtpPass { get; set; } = null!;
}