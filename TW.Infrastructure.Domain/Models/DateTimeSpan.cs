namespace TW.Infrastructure.Domain.Models;

public class DateTimeSpan
{
    public DateTimeSpan(DateTime? beginning = default, DateTime? ending = default)
    {
        if (beginning.HasValue && ending.HasValue && beginning > ending)
            throw new ArgumentException("beginning must not be greater than ending");

        Beginning = beginning;
        Ending = ending;
    }

    public DateTime? Beginning { get; protected set; }
    public DateTime? Ending { get; protected set; }

    public bool HasBeginning => Beginning.HasValue;
    public bool HasEnding => Ending.HasValue;
}