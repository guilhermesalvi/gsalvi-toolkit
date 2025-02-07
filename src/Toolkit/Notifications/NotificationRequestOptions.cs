using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Sozo.Toolkit.Notifications;

public record NotificationRequestOptions
{
    public RequestCulture? DefaultRequestCulture { get; set; }
    public IList<CultureInfo>? SupportedCultures { get; set; }
}
