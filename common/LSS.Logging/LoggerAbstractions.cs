using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LSS.Logging
{
    public class LoggerAbstractions
    {
        public static void SetupStaticLogger(string configurationPath)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configurationPath)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        public static IHostBuilder CreateHostBuilder()
        {
            return new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
            }).ConfigureLogging((hostContext, logging) =>
            {
                logging.AddSerilog();
            }
            );
        }
    }
}
