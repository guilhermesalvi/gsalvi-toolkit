using System.Collections.Immutable;

namespace Sozo.Toolkit.Notifications;

public sealed class NotificationManager
{
    private readonly HashSet<Notification> _notifications = [];

    /// <summary>
    /// Gets the collection of notifications.
    /// </summary>
    public IImmutableSet<Notification> Notifications => _notifications.ToImmutableHashSet();

    /// <summary>
    /// Checks whether the collection has any notifications.
    /// </summary>
    public bool HasNotifications => _notifications.Count != 0;

    /// <summary>
    /// Adds a new notification to the collection.
    /// </summary>
    /// <param name="notification"></param>
    public void AddNotification(Notification notification) =>
        _notifications.Add(notification);
}
