using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.Logger;
using LSS.BE.Core.Domain.Helpers;
using Serilog;

namespace LSS.BE.Core.Domain.Initialization
{
    public sealed class ServiceInvoke
    {
        public static TokenResponse InitAsync(string uriString, string version, string clientId, string clientSecret, string loggerConfigurationPath)
        {
            LoggerAbstractions.SetupStaticLogger(loggerConfigurationPath);
            LoggerAbstractions.CreateHostBuilder().Build();
            Log.Information("[Initiated][Service initiated with access token and logging.]");

            var tokenResponse = HttpHandlerHelper.GetToken(uriString, version, clientId, clientSecret);
            Log.Information("[Initialized][Service initialized with access token and logging.]");

            return tokenResponse;

        }
    }
}
