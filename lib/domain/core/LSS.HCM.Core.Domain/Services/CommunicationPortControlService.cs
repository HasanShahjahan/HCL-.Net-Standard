using LSS.HCM.Core.Common.Base;
using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Core.InputOutpuPorts;
using Microsoft.Win32.SafeHandles;
using Serilog;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace LSS.HCM.Core.Domain.Services
{
    /// <summary>
    ///   Represents communication protocol of com service.
    ///</summary>
    public class CommunicationPortControlService
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
        ///    Initialize scanner module.
        ///</summary>
        private readonly SerialPortControlService _scannerControlModule;

        // <summary>
        ///   Initialize lock module.
        ///</summary>
        private readonly SerialPortControlService _lockControlModule;

        // <summary>
        ///   Initialize object detection module.
        ///</summary>
        private readonly SerialPortControlService _detectionControlModule;

        /// <summary>
        ///   Initialize communication protocol of com service.
        ///</summary>
        public CommunicationPortControlService(AppSettings lockerConfiguration)
        {
            _scannerControlModule = new SerialPortControlService(new SerialPortResource(lockerConfiguration.Microcontroller.Scanner.Port, lockerConfiguration.Microcontroller.Scanner.Baudrate, lockerConfiguration.Microcontroller.Scanner.DataBits, 500, 500));
            _lockControlModule = new SerialPortControlService(new SerialPortResource(lockerConfiguration.Microcontroller.LockControl.Port, lockerConfiguration.Microcontroller.LockControl.Baudrate, lockerConfiguration.Microcontroller.LockControl.DataBits, 500, 500));
            _detectionControlModule = new SerialPortControlService(new SerialPortResource(lockerConfiguration.Microcontroller.ObjectDetection.Port, lockerConfiguration.Microcontroller.ObjectDetection.Baudrate, lockerConfiguration.Microcontroller.ObjectDetection.DataBits, 500, 500));
        }

        /// <summary>
        /// Send command by command name, command data with comparing locker configuration. 
        /// </summary>
        /// <returns>
        ///  Command result by initializing serial port for each command type. 
        /// </returns>
        public Dictionary<string, string> SendCommand(string commandName, List<byte> commandData, AppSettings lockerConfiguration)
        {
            List<byte> commandString = BoardInitializationService.GenerateCommand(commandName, commandData, lockerConfiguration);
            List<byte> responseData;
            Dictionary<string, string> result = null;
            Log.Information("[HCM][Communication Port Control Service][Send Command]" + "[Command Hex Value : " + commandString + "]");

            if (commandName == CommandType.DoorOpen)
            {
                _lockControlModule.Begin();
                responseData = _lockControlModule.Write(commandString, lockerConfiguration.Microcontroller.Commands.OpenDoor.ResLen);
                result = BoardInitializationService.ExecuteCommand(CommandType.DoorOpen, responseData);
                _lockControlModule.End();
            }
            else if (commandName == CommandType.DoorStatus)
            {
                _lockControlModule.Begin();
                responseData = _lockControlModule.Write(commandString, lockerConfiguration.Microcontroller.Commands.DoorStatus.ResLen);
                result = BoardInitializationService.ExecuteCommand(CommandType.DoorStatus, responseData);
                _lockControlModule.End();
            }
            else if (commandName == CommandType.ItemDetection)
            {
                _detectionControlModule.Begin();
                responseData = _detectionControlModule.Write(commandString, lockerConfiguration.Microcontroller.Commands.DetectItem.ResLen);
                result = BoardInitializationService.ExecuteCommand(CommandType.ItemDetection, responseData);
                _detectionControlModule.End();
            }
            Log.Information("[HCM][Communication Port Control Service][Send Command]" + "[Command Response Result : " + JsonSerializer.Serialize(result) + "]");

            Dictionary<string, string> commandResult = result;
            commandResult.Add("Command", commandName);

            return commandResult;
        }

        /// <summary>
        /// Initializes scanner from communication port to serial port by providing locker configuration. 
        /// </summary>
        /// <returns>
        ///  Publish scanning value.
        /// </returns>
        public void InitializeScanner(ComPortsHealthCheck comPortsHealthCheck, AppSettings lockerConfiguration, Func<string, string> dataProcessFunc)
        {
            if (comPortsHealthCheck.IsScannernPortAvailable)
            {
                Log.Information("[HCM][Communication Port Control Service][Initialize Scanner]" + "[Scanner Port : " + lockerConfiguration.Microcontroller.Scanner.Port + "]");
                _scannerControlModule.SetReadToPublishHandler(lockerConfiguration.Microcontroller.Scanner, dataProcessFunc);
                _scannerControlModule.Begin();
                
            }
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
                _scannerControlModule?.Dispose();
                _lockControlModule?.Dispose();
                _detectionControlModule?.Dispose();
            }

            _disposed = true;
        }
    }
}
