using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;
using Microsoft.Extensions.Logging;
using Master.Entity;
using Microsoft.Extensions.Configuration;

namespace Master
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.ConfigureLogging((context, logging) => { logging.ClearProviders(); });
                    webBuilder.UseKestrel((hostingContext, options) =>
                    {
                        LocalNetwork localNetworkConfig = hostingContext.Configuration.GetSection("localNetwork").Get<LocalNetwork>();
                        options.Listen(IPAddress.Loopback, localNetworkConfig.port);
                    });
                });
    }
}
