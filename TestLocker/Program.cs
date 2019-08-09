using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace TestLocker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    if (!context.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddUserSecrets<Startup>();
                    }

                    if (!context.HostingEnvironment.IsProduction()) return;

                    var builtConfig = builder.Build();

                    var keyVaultTokenProvider = new AzureServiceTokenProvider();

                    var keyVaultAuthenticationCallback = new KeyVaultClient.AuthenticationCallback(
                        keyVaultTokenProvider.KeyVaultTokenCallback);

                    var keyVaultClient = new KeyVaultClient(keyVaultAuthenticationCallback);

                    builder.AddAzureKeyVault(
                        $"https://{builtConfig["KeyVaultName"]}.vault.azure.net/",
                        keyVaultClient,
                        new DefaultKeyVaultSecretManager()
                        );
                });
    }
}
