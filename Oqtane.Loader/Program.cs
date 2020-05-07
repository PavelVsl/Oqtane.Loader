using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;

namespace Oqtane.Loader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var databaseManager = serviceScope.ServiceProvider.GetService<IDatabaseManager>();
                databaseManager.Install();
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<MyStartup>()
                .Build();
        }
    }

    // important for webassembly and use own _Host.cshtml
    public class MyStartup : Startup
    {
        public MyStartup(IWebHostEnvironment env) : base(env)
        {
        }
    }
}
