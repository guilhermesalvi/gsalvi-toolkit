using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace GSalvi.Toolkit.Notifications;

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

        services
            .Configure<RequestLocalizationOptions>(x =>
            {
                x.DefaultRequestCulture = notificationOptions.DefaultRequestCulture!;
                x.SupportedCultures = notificationOptions.SupportedCultures;
                x.SupportedUICultures = notificationOptions.SupportedCultures;
            })
            .AddLocalization();

        services.AddScoped<LocalizedNotificationManager>(sp =>
        {
            var factory = sp.GetRequiredService<IStringLocalizerFactory>();
            var localizer = factory.Create(
                typeof(TResource).FullName!,
                typeof(TResource).Assembly.GetName().Name!);
            return new LocalizedNotificationManager(localizer);
        });
    }

    public static void UseLocalizedNotifications(this WebApplication app) =>
        app.UseRequestLocalization();
}
