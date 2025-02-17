using Refit;

namespace Sozo.Toolkit.Tests.Hosting;

public interface IApiClient
{
    [Get("/api")]
    Task<string> GetAsync(
        [Query] string key,
        [Query] string args,
        [Query] string culture);
}
