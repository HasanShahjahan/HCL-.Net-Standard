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

namespace LSS.HCM.Core.Domain.Managers
{

    /// <summary>
    ///   Represents locker as a sequence of compartment open and it's status.
    ///</summary>
    public sealed class LockerManager : ILockerManager
    {
        /// <summary>
        ///   Set Initialization value for locker management.
        ///</summary>
        public readonly AppSettings lockerConfiguration;

        /// <summary>
        ///   Set Initialization value for Communication Port Health Check.
        ///</summary>
        public readonly ComPortsHealthCheck portsHealthCheck;

        /// <summary>
        ///   Initialization information for locker configuration including Microcontroller board, Serial port and Communication port.
        ///</summary>
        public LockerManager(string configurationFilePath)
        {
            var content = File.ReadAllText(configurationFilePath);
            lockerConfiguration = JsonSerializer.Deserialize<AppSettings>(content);
            portsHealthCheck = LockerHelper.ComPortTest(lockerConfiguration);
            Log.Information("[HCM][Locker Manager][Initiated][Service initiated with scanner and logging.]");
        }
        /// <summary>
        /// Scanner data recieving event
        /// </summary>
        public void RegisterScannerEvent(Func<string, string> dataProcessFunc)
        {
            CommunicationPortControlService.InitializeScanner(portsHealthCheck, lockerConfiguration, dataProcessFunc);

        }

        /// <summary>
        /// Gets the open compartment parameters with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment open status with object detection, LED status by requested compartment id's.
        /// </returns>
        public Dictionary<string,bool> PortHealthCheckStatus()
        {
            var response = new Dictionary<string, bool>();
            response.Add("locker", portsHealthCheck.IsLockPortAvailable);
            response.Add("detection", portsHealthCheck.IsDetectionPortAvailable);
            response.Add("scanner", portsHealthCheck.IsScannernPortAvailable);

            return response;
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
                if (!portsHealthCheck.IsLockPortAvailable && !portsHealthCheck.IsDetectionPortAvailable) return OpenCompartmentMapper.ToError(new LockerDto { StatusCode = StatusCode.Status503ServiceUnavailable, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.BrokenComPort, ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.BrokenComPort)) });
                
                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(lockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.OpenCompartment, model.LockerId, model.TransactionId, model.CompartmentIds, null);
                if (statusCode != StatusCode.Status200OK) return OpenCompartmentMapper.ToError(new LockerDto { StatusCode = statusCode, Error = errorResult });
                
                var result = CompartmentManager.CompartmentOpen(model, lockerConfiguration);
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
                if (!portsHealthCheck.IsLockPortAvailable && !portsHealthCheck.IsDetectionPortAvailable) return CompartmentStatusMapper.ToError(new CompartmentStatusDto { StatusCode = StatusCode.Status503ServiceUnavailable, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.BrokenComPort, ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.BrokenComPort)) });

                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(lockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.CompartmentStatus, model.LockerId, model.TransactionId, model.CompartmentIds, null);
                if (statusCode != StatusCode.Status200OK) return CompartmentStatusMapper.ToError(new CompartmentStatusDto { StatusCode = statusCode, Error = errorResult });
               
                var result = CompartmentManager.CompartmentStatus(model, lockerConfiguration);
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
                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(lockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.CaptureImage, model.LockerId, model.TransactionId, null, CaptureType.Photo);
                if (statusCode != StatusCode.Status200OK) return CaptureMapper.ToError(new CaptureDto { StatusCode = statusCode, Error = errorResult });
                var result = LockerHelper.CapturePhoto(model);
                captureDto = CaptureMapper.ToObject(result);
                Log.Information("[HCM][Capture Image][Res]" + "[" + JsonSerializer.Serialize(result) + "]");
            }
            catch (Exception ex)
            {
                Log.Error("[HCM][Capture Image]" + "[" + ex + "]");
                return CaptureMapper.ToError(new CaptureDto { StatusCode = StatusCode.Status500InternalServerError, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.InternalServerError, ex.Message) });
            }

            return captureDto;
        }
    }
}
