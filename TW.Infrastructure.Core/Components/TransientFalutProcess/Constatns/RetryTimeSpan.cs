namespace TW.Infrastructure.Core.Components.TransientFalutProcess;

public class RetryTimeSpan
{
    public static IReadOnlyCollection<TimeSpan> HIGH_FREQUENCY_BY_SECONDS = new[]
    {
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(1),
    };
    
    public static IReadOnlyCollection<TimeSpan> GENERAL_FREQUENCY_BY_SECONDS = new[]
    {
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(5),
        TimeSpan.FromSeconds(5),
    };
    
    public static IReadOnlyCollection<TimeSpan> LOW_FREQUENCY_BY_SECONDS = new[]
    {
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(10),
    };
}