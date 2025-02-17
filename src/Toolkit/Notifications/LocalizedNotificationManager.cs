using System.Collections.Immutable;
using Microsoft.Extensions.Localization;

namespace Sozo.Toolkit.Notifications;

public sealed class LocalizedNotificationManager(IStringLocalizer localizer)
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
    /// Adds a new notification to the collection using the specified key to retrieve the localized message.
    /// Args param are optional arguments to format the message.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="args"></param>
    public void AddNotification(string key, params object[] args)
    {
        var message = localizer[key, args];
        _notifications.Add(new Notification(message));
    }
}
