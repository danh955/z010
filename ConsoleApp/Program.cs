namespace ConsoleApp
{
    using System.Threading.Tasks;
    using App.Application;
    using App.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    internal class Program
    {
        private static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAppApplication();
                    services.AddAppInfrastructure("DataSource=:memory:");
                    services.AddHttpClient();
                    services.AddTransient<MainApp>();
                }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                // Run the main application.
                var rootApp = services.GetRequiredService<MainApp>();
                await rootApp.Start();
            }
        }
    }
}