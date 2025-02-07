namespace Sozo.Toolkit.Notifications;

public readonly record struct Notification(string Message)
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;

    public static implicit operator Notification(string message) => new(message);
}
