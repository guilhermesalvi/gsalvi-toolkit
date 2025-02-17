using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Sozo.Toolkit.Tests.Hosting;

public class AppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseContentRoot(Directory.GetCurrentDirectory());
    }
}
