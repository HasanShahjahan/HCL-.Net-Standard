using System;
using System.IO;
using System.Text.Json;
using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Mappers;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Services;
using LSS.HCM.Core.Validator;
using LSS.HCM.Core.Domain.Helpers;
using Compartment = LSS.HCM.Core.DataObjects.Models.Compartment;
using Serilog;
using LSS.HCM.Core.Domain.Interfaces;
using LSS.HCM.Core.Common.Base;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace LSS.HCM.Core.Domain.Managers
{

    /// <summary>
    ///   Represents locker as a sequence of compartment open and it's status.
    ///</summary>
    public class LockerManager : ILockerManager, IDisposable
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
        ///   Set Initialization value for locker management.
        ///</summary>
        public readonly AppSettings LockerConfiguration;

        /// <summary>
        ///   Set Initialization value for Communication Port Health Check.
        ///</summary>
        public readonly ComPortsHealthCheck PortsHealthCheck;

        private readonly CommunicationPortControlService _communicationPortControlService;
        private readonly CompartmentManager _compartmentManager;

        /// <summary>
        ///   Initialization information for locker configuration including Microcontroller board, Serial port and Communication port.
        ///</summary>
        public LockerManager(string configurationFilePath)
        {
            LockerConfiguration = LockerHelper.GetConfiguration(configurationFilePath);
            PortsHealthCheck = LockerHelper.ComPortTest(LockerConfiguration);
            if (LockerConfiguration != null) _communicationPortControlService = new CommunicationPortControlService(LockerConfiguration);
            if (LockerConfiguration != null) _compartmentManager = new CompartmentManager(LockerConfiguration);
            Log.Information("[HCM][Locker Manager][Initiated][Service initiated]");
        }

        /// <summary>
        /// Gets the open compartment parameters with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment open status with object detection, LED status by requested compartment id's.
        /// </returns>
        public LockerDto OpenCompartment(Compartment model)
        {
            var lockerDto = new LockerDto();
            Log.Information("[HCM][Open Compartment][Req]" + "[" + JsonSerializer.Serialize(model) + "]");

            try {
                if (!PortsHealthCheck.IsLockPortAvailable && !PortsHealthCheck.IsDetectionPortAvailable) return OpenCompartmentMapper.ToError(new LockerDto { StatusCode = StatusCode.Status503ServiceUnavailable, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.BrokenComPort, ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.BrokenComPort)) });
                
                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(LockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.OpenCompartment, model.LockerId, model.TransactionId, model.CompartmentIds, null);
                if (statusCode != StatusCode.Status200OK) return OpenCompartmentMapper.ToError(new LockerDto { StatusCode = statusCode, Error = errorResult });
                
                var result = _compartmentManager.CompartmentOpen(model, LockerConfiguration);
                lockerDto = OpenCompartmentMapper.ToObject(result);
                Log.Information("[HCM][Open Compartment][Res]" + "[" + JsonSerializer.Serialize(result) + "]");
            }
            catch (Exception ex)
            {
                Log.Error("[HCM][Open Compartment]" + "[" + ex + "]");
                return OpenCompartmentMapper.ToError(new LockerDto { StatusCode = StatusCode.Status500InternalServerError, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.InternalServerError, ex.Message) });
                
            }

            return lockerDto;
        }

        /// <summary>
        /// Gets the compartment status with Json web token credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment status with object detection and LED status based requested compartment id's.
        /// </returns>
        public CompartmentStatusDto CompartmentStatus(Compartment model)
        {
            var compartmentStatusDto = new CompartmentStatusDto();
            Log.Information("[HCM][Compartment Status][Req]" + "[" + JsonSerializer.Serialize(model) + "]");

            try
            {
                if (!PortsHealthCheck.IsLockPortAvailable && !PortsHealthCheck.IsDetectionPortAvailable) return CompartmentStatusMapper.ToError(new CompartmentStatusDto { StatusCode = StatusCode.Status503ServiceUnavailable, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.BrokenComPort, ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.BrokenComPort)) });

                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(LockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.CompartmentStatus, model.LockerId, model.TransactionId, model.CompartmentIds, null);
                if (statusCode != StatusCode.Status200OK) return CompartmentStatusMapper.ToError(new CompartmentStatusDto { StatusCode = statusCode, Error = errorResult });
               
                var result = _compartmentManager.CompartmentStatus(model, LockerConfiguration);
                compartmentStatusDto = CompartmentStatusMapper.ToObject(result);
                Log.Information("[HCM][Compartment Status][Res]" + "[" + JsonSerializer.Serialize(result) + "]");
            }
            catch (Exception ex)
            {
                Log.Error("[HCM][Compartment Status]" + "[" + ex + "]");
                return CompartmentStatusMapper.ToError(new CompartmentStatusDto { StatusCode = StatusCode.Status500InternalServerError, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.InternalServerError, ex.Message) });
            }

            return compartmentStatusDto;
        }

        /// <summary>
        /// Gets the capture image parameters with Json web token credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  Byte array of image with transaction Id and image extension.
        /// </returns>
        public CaptureDto CaptureImage(Capture model)
        {
            var captureDto = new CaptureDto();
            Log.Information("[HCM][Capture Image][Req]" + "[" + JsonSerializer.Serialize(model) + "]");

            try
            {
                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(LockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.CaptureImage, model.LockerId, model.TransactionId, null, CaptureType.Photo);
                if (statusCode != StatusCode.Status200OK) return CaptureMapper.ToError(new CaptureDto { StatusCode = statusCode, Error = errorResult });
                var result = LockerHelper.CapturePhoto(model, LockerConfiguration);
                captureDto = CaptureMapper.ToObject(result);
                Log.Information("[HCM][Capture Image][Res]" + "[Success]");
            }
            catch (Exception ex)
            {
                Log.Error("[HCM][Capture Image]" + "[" + ex + "]");
                return CaptureMapper.ToError(new CaptureDto { StatusCode = StatusCode.Status500InternalServerError, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.InternalServerError, ex.Message) });
            }

            return captureDto;
        }

        /// <summary>
        /// Scanner data recieving event
        /// </summary>
        public void RegisterScannerEvent(Func<string, string> dataProcessFunc)
        {
            _communicationPortControlService.InitializeScanner(PortsHealthCheck, LockerConfiguration, dataProcessFunc);
        }

        /// <summary>
        /// Gets the open compartment parameters with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment open status with object detection, LED status by requested compartment id's.
        /// </returns>
        public Dictionary<string, bool> PortHealthCheckStatus()
        {
            var response = new Dictionary<string, bool>
            {
                { "locker", PortsHealthCheck.IsLockPortAvailable },
                { "detection", PortsHealthCheck.IsDetectionPortAvailable },
                { "scanner", PortsHealthCheck.IsScannernPortAvailable }
            };

            return response;
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
                _communicationPortControlService.Dispose();
                _compartmentManager?.Dispose();
            }

            _disposed = true;
        }
    }
}
