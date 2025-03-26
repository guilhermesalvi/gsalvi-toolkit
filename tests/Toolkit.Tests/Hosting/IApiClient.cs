using Refit;

namespace GSalvi.Toolkit.Tests.Hosting;

public interface IApiClient
{
    [Get("/api")]
    Task<string> GetAsync(
        [Query] string key,
        [Query] string args,
        [Query] string culture);
}
