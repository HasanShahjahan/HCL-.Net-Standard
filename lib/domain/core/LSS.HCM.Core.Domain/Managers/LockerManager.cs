using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Mappers;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Services;
using LSS.HCM.Core.Validator;
using System;
using System.IO;
using System.Text.Json;

// Important: include the opencvsharp library in your code
using OpenCvSharp;
using OpenCvSharp.Extensions;

using ImageEnt = LSS.HCM.Core.Entities.Locker.Image;
using Compartment = LSS.HCM.Core.DataObjects.Models.Compartment;
using System.Drawing;
using System.Drawing.Imaging;

namespace LSS.HCM.Core.Domain.Managers
{

    /// <summary>
    ///   Represents locker as a sequence of compartment open and it's status.
    ///</summary>
    public sealed class LockerManager
    {
        /// <summary>
        ///   Set Initialization value for locker management.
        ///</summary>
        private readonly AppSettings lockerConfiguration;

        /// <summary>
        ///   Initialization information for locker configuration including Microcontroller board, Serial port and Communication port.
        ///</summary>
        public LockerManager(string configurationFilePath)
        {
            var content = File.ReadAllText(configurationFilePath);
            lockerConfiguration = JsonSerializer.Deserialize<AppSettings>(content);

            CommunicationPortControlService.SerialScannerInputHandler(lockerConfiguration);
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

            try {
                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(lockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.OpenCompartment, model.LockerId, model.TransactionId, model.CompartmentIds, null);
                if (statusCode != StatusCode.Status200OK) return OpenCompartmentMapper.ToError(new LockerDto { StatusCode = statusCode, Error = errorResult });
                var result = CompartmentManager.CompartmentOpen(model, lockerConfiguration);
                lockerDto = OpenCompartmentMapper.ToObject(result);
            }
            catch (Exception ex)
            {
                return OpenCompartmentMapper.ToError(new LockerDto { StatusCode = StatusCode.Status500InternalServerError, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.InternalServerError, ex.Message) });
            }

            return lockerDto;
        }

        /// <summary>
        /// Gets the compartment status with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment status with object detection and LED status based requested compartment id's.
        /// </returns>
        public CompartmentStatusDto CompartmentStatus(Compartment model)
        {
            var compartmentStatusDto = new CompartmentStatusDto();
            try
            {
                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(lockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.CompartmentStatus, model.LockerId, model.TransactionId, model.CompartmentIds, null);
                if (statusCode != StatusCode.Status200OK) return CompartmentStatusMapper.ToError(new CompartmentStatusDto { StatusCode = statusCode, Error = errorResult });
                var result = CompartmentManager.CompartmentStatus(model, lockerConfiguration);
                compartmentStatusDto = CompartmentStatusMapper.ToObject(result);
            }
            catch (Exception ex)
            {
                return CompartmentStatusMapper.ToError(new CompartmentStatusDto { StatusCode = StatusCode.Status500InternalServerError, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.InternalServerError, ex.Message) });
            }

            return compartmentStatusDto;
        }


        public CaptureDto CaptureImage(Capture model)
        {
            var photoDto = new CaptureDto();
            try
            {
                var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(lockerConfiguration, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.CaptureImage, model.LockerId, model.TransactionId, null, Common.Enums.CaptureType.Photo);
                if (statusCode != StatusCode.Status200OK) return CaptureMapper.ToError(new CaptureDto { StatusCode = statusCode, Error = errorResult });
                var result = CapturePhoto(model, lockerConfiguration);
                photoDto = CaptureMapper.ToObject(result);
            }
            catch (Exception ex)
            {
                return CaptureMapper.ToError(new CaptureDto { StatusCode = StatusCode.Status500InternalServerError, Error = new Common.Exceptions.ApplicationException(ApplicationErrorCodes.InternalServerError, ex.Message) });
            }


            return photoDto;
        }

        private static CaptureDto CapturePhoto(DataObjects.Models.Capture model, AppSettings lockerConfiguration)
        {
            VideoCapture capture;
            Mat frame;
            Bitmap imageBitmapData;
            byte[] imageBytes = null;
            //bool isCameraRunning = false;

            frame = new Mat();
            capture = new VideoCapture(0);
            capture.Open(0);

            if (capture.IsOpened())
            {
                capture.Read(frame);
                imageBitmapData = BitmapConverter.ToBitmap(frame);
                Bitmap snapshot = new Bitmap(imageBitmapData);

                snapshot.Save(string.Format(@"D:\{0}.jpeg", Guid.NewGuid()), ImageFormat.Jpeg);
                imageBytes = ToByteArray(snapshot, ImageFormat.Jpeg);
            }
            else
            {
                Console.WriteLine("Cannot take picture if the camera isn't capturing image!");
            }
            ImageEnt captureImage = new ImageEnt(model.Image.ImageExtension, imageBytes);

            CaptureDto captureResponse = new CaptureDto(model.TransactionId, model.LockerId, captureImage);

            // End using camera
            capture.Release();

            return captureResponse;
        }
        public static byte[] ToByteArray(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}
