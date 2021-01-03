using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.Exceptions;
using LSS.BE.Core.Common.Utiles;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Domain.Interfaces;
using LSS.Common.Logging;
using Serilog;
using System;

namespace LSS.BE.Core.Domain.Initialization
{
    /// <summary>
    ///   Represents logger library creation, host build creating and Basic token authentication.
    ///</summary>
    public sealed class ServiceInvoke
    {
        /// <summary>
        /// Initialize logger library, create host build and asic token authentication.
        /// </summary>
        /// <returns>
        ///  Gets the token result. 
        /// </returns>
        public static TokenResponse InitAsync(MemberInfo memberInfo, IHttpHandlerHelper httpHandler)
        {
            var tokenResponse = new TokenResponse();
            try 
            {
                if (Utiles.IsValidPath(memberInfo.ConfigurationPath))
                {
                    LoggerAbstractions.SetupStaticLogger(memberInfo);
                    Log.Information("[Initialization][Service initiated with logging.]");

                    tokenResponse = httpHandler.GetToken(memberInfo.Version, memberInfo.ClientId, memberInfo.ClientSecret);
                    Log.Information("[Initialized][Service initialized with access token]");
                }
                else
                {
                    tokenResponse.StatusCode = StatusCode.Status412PreconditionFailed;
                }
                
            }
            catch (Exception ex) 
            {
                Log.Error("[Initialization][Failed]" + ex);
                tokenResponse.StatusCode = StatusCode.Status502BadGateway;
            }

            return tokenResponse;
        }
    }
}
