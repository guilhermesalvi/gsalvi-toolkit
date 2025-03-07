using System.Collections.Immutable;
using Microsoft.Extensions.Localization;

namespace Sozo.Toolkit.Notifications;

public sealed class LocalizedNotificationManager(IStringLocalizer localizer)
{
    private readonly HashSet<Notification> _notifications = [];

    public IImmutableSet<Notification> Notifications => _notifications.ToImmutableHashSet();
    public bool HasNotifications => _notifications.Count != 0;

    public void AddNotification(string key, params object[] args)
    {
        var message = localizer[key, args];
        _notifications.Add(new Notification(key, message));
    }
}
