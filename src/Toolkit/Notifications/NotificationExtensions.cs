using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Sozo.Toolkit.Notifications;

public static class NotificationExtensions
{
    /// <summary>
    /// Registers the simple notification manager without localization.
    /// </summary>
    /// <param name="services"></param>
    private static void AddNotifications(
        this IServiceCollection services)
    {
        services.AddScoped<NotificationManager>();
    }

    /// <summary>
    /// Registers the localized notification manager with the specified resource type.
    /// Tip: See the official docs for more information on how to create resource files.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <typeparam name="TResource"></typeparam>
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

    /// <summary>
    /// Enables the localized notifications in the request pipeline.
    /// </summary>
    /// <param name="app"></param>
    public static void UseLocalizedNotifications(this WebApplication app) =>
        app.UseRequestLocalization();
}
