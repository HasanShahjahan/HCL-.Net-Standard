using LSS.BE.Core.Domain.Initialization;
using LSS.BE.Core.Domain.Interfaces;
using LSS.BE.Core.Domain.Services;
using LSS.HCM.Core.Domain.Interfaces;
using LSS.HCM.Core.Domain.Managers;
using LSS.HCM.Core.Domain.Services;
using Microsoft.Win32.SafeHandles;
using Serilog;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LSS.BE.Core.Domain.Helpers
{
    /// <summary>
    ///   Represents scanner serive helper for start socket client.
    ///</summary>
    public class ScannerServiceHelper 
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
        ///   Initialize the socket client for invoker.
        ///</summary>
        private static SocketClientInvoke _client;

        /// <summary>
        /// Start socket client by connecting and registering event. 
        /// </summary>
        /// <returns>
        ///  Gets the sucess result with bool type true. 
        /// </returns>
        public static bool Start(LockerManager _lockerManager)
        {
            bool status = false;
            try
            {
                _client = new SocketClientInvoke(_lockerManager.LockerConfiguration.Socket.Server, _lockerManager.LockerConfiguration.Socket.Port);
                status = _client.Connect();
                if (status) 
                {
                    _lockerManager.RegisterScannerEvent(SendDataOnSocket);
                    Log.Information("[Scanner Service Helper][Start][Scanner Initialization is successful.]");
                }
                
                return status;
            }
            catch (Exception ex)
            {
                Log.Error("[Scanner Service Helper][Start][Scanner Initialization is failed.]" + "[" + ex + "]");
                return status;
            }

        }

        /// <summary>
        /// Send data on socket.
        /// </summary>
        /// <returns>
        ///  Gets the input data. 
        /// </returns>
        private static string SendDataOnSocket(string inputData)
        {
            _client.Send("scanner," + inputData);
            return inputData;
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
                _safeHandle?.Dispose();
                _client?.Dispose();
            }

            _disposed = true;
        }
    }
}
