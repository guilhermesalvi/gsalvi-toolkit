# Sozo.Toolkit

[![NuGet Version](https://img.shields.io/nuget/v/Sozo.Toolkit.svg)](https://www.nuget.org/packages/Sozo.Toolkit)  
[![Build, Test, and Publish Package (Beta or Stable)](https://github.com/guilhermesalvi/sozo-toolkit/actions/workflows/main.yml/badge.svg)](https://github.com/guilhermesalvi/sozo-toolkit/actions/workflows/main.yml)

**Sozo.Toolkit** is a versatile .NET utility library that brings together a set of helpers and extension methods to accelerate your development. This package includes:

- **String Extensions:** Remove diacritics, specific characters, duplicate spaces, and non-alphanumeric characters.
- **Logging Utilities:** Easily add custom properties to your Serilog logs with a fluent builder.
- **Notification Management:** Create and manage notifications (both simple and localized) with immutable notification records.
- **Validators:** Validate strings for allowed characters, digits, letters, and email addresses.

---

## 📦 Features

### 🔠 String Extensions

- **RemoveDiacritics:** Strips accents and diacritics from text.
- **RemoveSpecificChars:** Removes only the specified characters from a string.
- **RemoveCharsNotIn:** Keeps only characters from an allowed set.
- **RemoveDuplicateSpaces:** Eliminates duplicate spaces, leaving only a single space between words.
- **RemoveNonAlphanumeric:** Filters out non-alphanumeric characters with the option to allow additional characters.

### 🔧 Logging Utilities

- **LogEnricherBuilder:** Build custom enrichers for Serilog easily by adding key/value pairs that will be attached to your logs.

### 🔔 Notification Management

- **NotificationManager:** Manages simple notifications.
- **LocalizedNotificationManager:** Supports localized notifications using ASP.NET Core's localization features.

### ✅ Validators

- **CharsValidator:** Check if a string contains only allowed characters, only digits, or only letters.
- **EmailValidator:** Validate email addresses using a robust, pre-compiled regular expression.

---

## ⚙️ Installation

Install the package via the [NuGet Package Manager](https://www.nuget.org/) using:

```bash
Install-Package Sozo.Toolkit
```

Or with the .NET CLI:

```bash
dotnet add package Sozo.Toolkit
```

---

## 🚀 Usage Examples

### 1. String Extensions

```csharp
using Sozo.Toolkit.Extensions;

string accentedText = "Às ínclitas maçãs, o coração exibia êxtase, paixão e júbilo; já a fênix, à luz do pôr-do-sol, encantava o céu.";
string plainText = accentedText.RemoveDiacritics(); // Returns: "As inclitas macas, o coracao exibia extase, paixao e jubilo; ja a fenix, a luz do por-do-sol, encantava o ceu."

string textWithVowels = "Às ínclitas maçãs";
string withoutVowels = textWithVowels.RemoveSpecificChars('a', 'e', 'i', 'o', 'u'); // Returns: "Às ínclts mçãs"

string textWithDuplicates = "Às  ínclitas      maçãs";
string cleanedText = textWithDuplicates.RemoveDuplicateSpaces(); // Returns: "Às ínclitas maçãs"

string mixedText = "Às ínclitas maçãs, 123!.";
string alphaNumericOnly = mixedText.RemoveNonAlphanumeric(); // Returns: "Àsínclitasmaçãs123"
```

### 2. Logging with Serilog

```csharp
using Serilog;
using Sozo.Toolkit.Logging;

var enricherBuilder = new LogEnricherBuilder()
    .WithProperty("Application", "MyApp")
    .WithProperty("Environment", "Production");

Log.Logger = new LoggerConfiguration()
    .Enrich.With(enricherBuilder)
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Application started.");
```

### 3. Notification Management

#### Simple Notifications

```csharp
using Sozo.Toolkit.Notifications;

var notificationManager = new NotificationManager();
notificationManager.AddNotification("Operation completed successfully.");

if (notificationManager.HasNotifications)
{
    foreach (var notification in notificationManager.Notifications)
    {
        Console.WriteLine(notification.Message);
    }
}
```

#### Localized Notifications in ASP.NET Core

Create a dummy class to be used as a type argument for the factory.Create method:

```csharp
public abstract class SharedResource;
```

Create your resource files in the Resources folder:

```
Resources/
    SharedResource.cs
    SharedResource.en-US.resx
    SharedResource.pt-BR.resx
```

Configure services in your `Program.cs` or `Startup.cs`:

```csharp
using Microsoft.AspNetCore.Localization;
using Sozo.Toolkit.Notifications;
using System.Globalization;

builder.Services.AddLocalizedNotifications<SharedResource>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("pt-BR");
    options.SupportedCultures = [new CultureInfo("pt-BR"), new CultureInfo("en-US")];
});

var app = builder.Build();
app.UseLocalizedNotifications();

```

Using the localized notification manager in your application:

```csharp
public class MyService(LocalizedNotificationManager notificationManager)
{
    public void Execute()
    {
        // 'UserNotFound' should be a key defined in your resource files.
        _notificationManager.AddNotification("UserNotFound", "john doe");

        // Access notifications
        foreach (var notification in _notificationManager.Notifications)
        {
            Console.WriteLine($"{notification.Id}: {notification.Message} at {notification.Timestamp}");
        }
    }
}

```

### 4. Validators

```csharp
using Sozo.Toolkit.Validators;

// Check if a string contains only allowed characters
bool isValid = CharsValidator.HasOnlyAllowedChars("abc", 'a', 'b', 'c'); // Returns: true

// Validate if a string has only digits
bool onlyDigits = CharsValidator.HasOnlyDigits("123456"); // Returns: true

// Validate email addresses
bool validEmail = EmailValidator.IsEmailAddress("test@example.com"); // Returns: true

```

---

Enjoy coding with Sozo.Toolkit! ✨
