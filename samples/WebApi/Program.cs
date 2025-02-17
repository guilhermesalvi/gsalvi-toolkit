using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Sozo.Toolkit.Notifications;
using WebApi.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddLocalizedNotifications<SharedResource>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = [new CultureInfo("pt-BR"), new CultureInfo("en-US")];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseLocalizedNotifications();

app.UseHttpsRedirection();

app.MapGet("/", (LocalizedNotificationManager manager) =>
{
    manager.AddNotification("UserNotFound", "john doe");
    var message = manager.Notifications.First().Message;
    return TypedResults.Ok(message);
});

app.Run();
