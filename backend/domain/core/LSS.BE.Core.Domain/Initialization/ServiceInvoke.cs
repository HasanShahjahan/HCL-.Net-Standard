using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.Exceptions;
using LSS.BE.Core.Common.Utiles;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Domain.Interfaces;
using LSS.Common.Logging;
using Microsoft.Win32.SafeHandles;
using Serilog;
using System;
using System.Runtime.InteropServices;

namespace LSS.BE.Core.Domain.Initialization
{
    /// <summary>
    ///   Represents logger library creation, host build creating and Basic token authentication.
    ///</summary>
    public class ServiceInvoke : IDisposable
    {
        /// <summary>
        ///   To detect redundant calls.
        ///</summary>
        private bool _disposed = false;

        /// <summary>
        ///   Instantiate a SafeHandle instance.
        ///</summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        ///   Set gateway initialization member value.
        ///</summary>
        private readonly MemberInfo _memberInfo;

        /// <summary>
        ///   Set gateway initialization http handler value.
        ///</summary>
        private readonly HttpHandlerHelper _httpHandler;

        /// <summary>
        ///   Represents logger library creation, host build creating and Basic token authentication.
        ///</summary>
        public ServiceInvoke(MemberInfo memberInfo, HttpHandlerHelper httpHandler)
        {
            _memberInfo = memberInfo;
            _httpHandler = httpHandler;
        }

        /// <summary>
        /// Initialize logger library, create host build and asic token authentication.
        /// </summary>
        /// <returns>
        ///  Gets the token result. 
        /// </returns>
        public TokenResponse InitAsync()
        {
            var tokenResponse = new TokenResponse();
            try 
            {
                if (Utiles.IsValidPath(_memberInfo.ConfigurationPath))
                {
                    LoggerAbstractions.SetupStaticLogger(_memberInfo);
                    Log.Information("[Initialization][Service initiated with logging.]");

                    tokenResponse = _httpHandler.GetToken(_memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret);
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

        /// <summary>
        ///   Public implementation of Dispose pattern callable by consumers.
        ///</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Protected implementation of Dispose pattern.
        ///</summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }
    }
}
