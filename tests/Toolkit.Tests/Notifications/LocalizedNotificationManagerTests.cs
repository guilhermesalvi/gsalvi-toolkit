using FluentAssertions;
using Microsoft.Extensions.Localization;
using NSubstitute;
using Sozo.Toolkit.Notifications;
using Sozo.Toolkit.Tests.Hosting;

namespace Sozo.Toolkit.Tests.Notifications;

[Collection(nameof(FixtureCollection))]
public class LocalizedNotificationManagerTests(TestHost host)
{
    [Theory]
    [InlineData("pt-BR", "Usuário 'john doe' não encontrado.")]
    [InlineData("en-US", "User 'john doe' not found.")]
    public async Task Add_notification_to_the_list(string culture, string expectedMessage)
    {
        // Arrange
        const string key = "UserNotFound";
        const string args = "john doe";

        // Act
        var message = await host.ApiClient.GetAsync(key, args, culture);

        // Assert
        message.Should().Be(expectedMessage);
    }

    [Fact]
    public void HasNotifications_ShouldReturnFalse_WhenNoNotificationIsAdded()
    {
        // Arrange
        var localizer = Substitute.For<IStringLocalizer>();
        var manager = new LocalizedNotificationManager(localizer);

        // Act & Assert
        manager.HasNotifications.Should().BeFalse();
        manager.Notifications.Should().BeEmpty();
    }

    [Fact]
    public void AddNotification_ShouldAddNotification_WithLocalizedMessageAndValidNotificationProperties()
    {
        // Arrange
        var localizer = Substitute.For<IStringLocalizer>();
        const string key = "TestKey";
        var args = new object[] { "Arg1" };
        const string expectedMessage = "Localized message with Arg1";
        localizer[key, args].Returns(new LocalizedString(key, expectedMessage));

        var manager = new LocalizedNotificationManager(localizer);

        // Act
        manager.AddNotification(key, args);

        // Assert
        manager.HasNotifications.Should().BeTrue();
        var notification = manager.Notifications.First();
        notification.Message.Should().Be(expectedMessage);
        notification.Id.Should().NotBeEmpty();
        notification.Timestamp.Should().NotBe(default);
    }

    [Fact]
    public void AddNotification_ShouldAddMultipleNotifications_WhenCalledMultipleTimes()
    {
        // Arrange
        var localizer = Substitute.For<IStringLocalizer>();
        const string key1 = "Key1";
        const string key2 = "Key2";
        const string expectedMessage1 = "Localized message 1";
        const string expectedMessage2 = "Localized message 2";

        localizer[key1, Arg.Any<object[]>()].Returns(new LocalizedString(key1, expectedMessage1));
        localizer[key2, Arg.Any<object[]>()].Returns(new LocalizedString(key2, expectedMessage2));

        var manager = new LocalizedNotificationManager(localizer);

        // Act
        manager.AddNotification(key1);
        manager.AddNotification(key2);

        // Assert
        manager.HasNotifications.Should().BeTrue();
        var notifications = manager.Notifications;
        notifications.Should().HaveCount(2);
        notifications.Any(n => n.Message == expectedMessage1).Should().BeTrue();
        notifications.Any(n => n.Message == expectedMessage2).Should().BeTrue();
    }

    [Fact]
    public void Notifications_ShouldReturnAnImmutableSet()
    {
        // Arrange
        var localizer = Substitute.For<IStringLocalizer>();
        const string key = "TestKey";
        const string expectedMessage = "Localized message";
        localizer[key, Arg.Any<object[]>()].Returns(new LocalizedString(key, expectedMessage));

        var manager = new LocalizedNotificationManager(localizer);
        manager.AddNotification(key);

        // Act
        var notifications = manager.Notifications;

        // Assert
        notifications.Should().NotBeNull();
        notifications.Should().HaveCount(1);
    }
}
