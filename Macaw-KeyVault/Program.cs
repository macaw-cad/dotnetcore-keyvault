using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Macaw_KeyVault
{
    public class Program
    {
        private const string KeyVaultUri = "KeyVault:Uri";
        private const string KeyVaultClientId = "KeyVault:ClientId";
        private const string KeyVaultClientSecrect = "KeyVault:ClientSecret";

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    var config = configurationBuilder.Build();

                    configurationBuilder.AddAzureKeyVault(
                        config[KeyVaultUri],
                        config[KeyVaultClientId],
                        config[KeyVaultClientSecrect]
                    );
                })
                .UseStartup<Startup>();
    }
}