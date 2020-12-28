using LSS.BE.Core.Domain.Initialization;
using LSS.BE.Core.Domain.Interfaces;
using LSS.HCM.Core.Domain.Interfaces;
using LSS.HCM.Core.Domain.Managers;
using LSS.HCM.Core.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Domain.Helpers
{
    /// <summary>
    ///   Represents scanner serive helper for start socket client.
    ///</summary>
    public class ScannerServiceHelper
    {
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
            try
            {
                _client = new SocketClientInvoke(_lockerManager.LockerConfiguration.Socket.Server, _lockerManager.LockerConfiguration.Socket.Port);
                _client.Connect();
                _lockerManager.RegisterScannerEvent(SendDataOnSocket);
                return true;
            }
            catch (Exception)
            {
                return false;
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
    }
}
