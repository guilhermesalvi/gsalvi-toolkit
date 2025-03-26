namespace GSalvi.Toolkit.Notifications;

public readonly record struct Notification(string Key, string Value)
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
}
