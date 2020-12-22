using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace LSS.BE.Core.Common.Logger
{
    public class LoggerAbstractions
    {
		public static void SetupStaticLogger(string loggerConfigurationPath)
		{
            var configuration = new ConfigurationBuilder()
				.AddJsonFile(loggerConfigurationPath)
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
                //services.AddTransient(typeof(ClassThatLogs));
            }).ConfigureLogging((hostContext, logging) =>
            {
                logging.AddSerilog();
            }
            );
        }
    }
}
