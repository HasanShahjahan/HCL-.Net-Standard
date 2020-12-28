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
    public class ScannerServiceHelper
    {
        private static SocketClientInvoke _client;
        public static bool Start(LockerManager _lockerManager)
        {
            try
            {
                _client = new SocketClientInvoke(_lockerManager.lockerConfiguration.Socket.Server, _lockerManager.lockerConfiguration.Socket.Port);
                _client.Connect();
                _lockerManager.RegisterScannerEvent(SendDataOnSocket);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private static string SendDataOnSocket(string inputData)
        {
            _client.Send("scanner," + inputData);
            return inputData;
        }
    }
}
