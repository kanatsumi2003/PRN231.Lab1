using Microsoft.Extensions.Configuration;

namespace Petalaka.Account.Core.Utils;

public static class ReadConfiguration
{
    public static IConfiguration ReadAppSettings()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Petalaka.Account.Api")))
            .AddJsonFile("appsettings.json")
            .Build();
        return configuration;
    }    
}