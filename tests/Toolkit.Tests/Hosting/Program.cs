using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using GSalvi.Toolkit.Notifications;
using GSalvi.Toolkit.Tests.Hosting.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalizedNotifications<SharedResource>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = [new CultureInfo("pt-BR"), new CultureInfo("en-US")];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseLocalizedNotifications();

app.UseHttpsRedirection();

app.MapGet("/api", (LocalizedNotificationManager manager, string key, string args) =>
{
    manager.AddNotification(key, args);
    var message = manager.Notifications.First().Value;
    return Results.Text(message);
});

app.Run();

public partial class Program;
