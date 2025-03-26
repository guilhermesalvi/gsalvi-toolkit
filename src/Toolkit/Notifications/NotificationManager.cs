using System.Collections.Immutable;

namespace GSalvi.Toolkit.Notifications;

public sealed class NotificationManager
{
    private readonly HashSet<Notification> _notifications = [];

    public IImmutableSet<Notification> Notifications => _notifications.ToImmutableHashSet();
    public bool HasNotifications => _notifications.Count != 0;

    public void AddNotification(Notification notification) =>
        _notifications.Add(notification);
}
