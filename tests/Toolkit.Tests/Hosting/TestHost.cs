using Refit;

namespace Sozo.Toolkit.Tests.Hosting;

public sealed class TestHost : IDisposable
{
    private readonly AppFactory _factory;
    private readonly HttpClient _client;
    public readonly IApiClient ApiClient;

    private bool _disposed;

    public TestHost()
    {
        _factory = new AppFactory();
        _client = _factory.CreateClient();
        ApiClient = RestService.For<IApiClient>(_client);
    }

    public void Dispose()
    {
        if (_disposed) return;

        _client.Dispose();
        _factory.Dispose();
        _disposed = true;
    }
}
