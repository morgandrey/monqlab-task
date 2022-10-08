namespace MonqlabTask.Helpers;

public class AppSettings
{
    /// <summary>
    /// The refresh token TTL.
    /// </summary>
    public int RefreshTokenTTL { get; set; }
    
    /// <summary>
    /// The email from.
    /// </summary>
    public string EmailFrom { get; set; } = null!;
    
    /// <summary>
    /// The smtp host.
    /// </summary>
    public string SmtpHost { get; set; } = null!;
    
    /// <summary>
    /// The smtp port.
    /// </summary>
    public int SmtpPort { get; set; }
    
    /// <summary>
    /// The smtp user.
    /// </summary>
    public string SmtpUser { get; set; } = null!;
    
    /// <summary>
    /// The smtp password.
    /// </summary>
    public string SmtpPass { get; set; } = null!;
}