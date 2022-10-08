namespace MonqlabTask.Helpers;

public interface IDateTimeProvider
{
    /// <summary>
    /// The date time now.
    /// </summary>
    DateTime Now { get; }
}

/// <summary>
/// Datetime provider class.
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime Now => DateTime.Now;
}