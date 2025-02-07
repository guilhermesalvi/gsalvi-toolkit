using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Sozo.Toolkit.Notifications;

public static class NotificationExtensions
{
    private static void AddNotifications(
        this IServiceCollection services)
    {
        services.AddScoped<NotificationManager>();
    }

    public static void AddLocalizedNotifications<TResource>(
        this IServiceCollection services,
        Action<NotificationOptions> setupAction)
        where TResource : class
    {
        var notificationOptions = new NotificationOptions();
        setupAction.Invoke(notificationOptions);

        if (string.IsNullOrWhiteSpace(notificationOptions.ResourcesPath))
            throw new InvalidOperationException("ResourcesPath is required.");

        services.AddLocalization(options => options.ResourcesPath = notificationOptions.ResourcesPath);

        services.AddScoped<LocalizedNotificationManager>(sp =>
        {
            var factory = sp.GetRequiredService<IStringLocalizerFactory>();
            var localizer = factory.Create(
                typeof(TResource).FullName!,
                typeof(TResource).Assembly.GetName().Name!);
            return new LocalizedNotificationManager(localizer);
        });
    }

    public static void UseLocalizedNotifications(
        this WebApplication app,
        Action<NotificationRequestOptions> setupAction)
    {
        var requestOptions = new NotificationRequestOptions();
        setupAction.Invoke(requestOptions);

        if (requestOptions.DefaultRequestCulture is null)
            throw new InvalidOperationException("DefaultRequestCulture is required.");

        if (requestOptions.SupportedCultures is null)
            throw new InvalidOperationException("SupportedCultures is required.");

        app.UseRequestLocalization(options =>
        {
            options.DefaultRequestCulture = requestOptions.DefaultRequestCulture!;
            options.SupportedCultures = requestOptions.SupportedCultures;
            options.SupportedUICultures = requestOptions.SupportedCultures;
        });
    }
}
