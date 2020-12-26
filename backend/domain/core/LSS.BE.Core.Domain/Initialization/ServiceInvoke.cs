using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Domain.Interfaces;
using LSS.Logging;
using Serilog;

namespace LSS.BE.Core.Domain.Initialization
{
    public sealed class ServiceInvoke
    {
        public static TokenResponse InitAsync(string uriString, string version, string clientId, string clientSecret, string configurationPath, IHttpHandlerHelper httpHandler)
        {
            LoggerAbstractions.SetupStaticLogger(configurationPath);
            LoggerAbstractions.CreateHostBuilder().Build();
            Log.Information("[Initialization][Service initiated with access token and logging.]");

            var tokenResponse = httpHandler.GetToken(uriString, version, clientId, clientSecret);
            Log.Information("[Initialized][Service initialized with access token and logging.]");

            return tokenResponse;

        }
    }
}
