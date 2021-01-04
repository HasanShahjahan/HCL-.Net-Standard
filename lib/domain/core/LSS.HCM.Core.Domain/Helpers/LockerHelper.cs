using LSS.HCM.Core.Common.Base;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Services;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Serilog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace LSS.HCM.Core.Domain.Helpers
{

    /// <summary>
    ///   Represents locker manager helper.
    ///</summary>
    public class LockerHelper
    {
        /// <summary>
        /// Image byte array preparation by open library.
        /// </summary>
        /// <returns>
        ///  Byte array of image with image extension.
        /// </returns>
        public static CaptureDto CapturePhoto(DataObjects.Models.Capture model, AppSettings lockerConfiguration)
        {
            byte[] imageBytes = null;
            Mat frame = new Mat();
            using (VideoCapture capture = new VideoCapture(0))
            {
                capture.Open(0);

                if (capture.IsOpened())
                {
                    capture.Read(frame);
                    Bitmap imageBitmapData = BitmapConverter.ToBitmap(frame);
                    Bitmap snapshot = new Bitmap(imageBitmapData);
                    string photofileName = string.Format(@"{0}.jpeg", lockerConfiguration.Locker.LockerId + "_" + Guid.NewGuid());

                    //This saved real picture to physical location. 
                    if (lockerConfiguration.CaptureImage.IsSnapShotToLocal) snapshot.Save(photofileName, ImageFormat.Jpeg); 
                    imageBytes = ToByteArray(snapshot, ImageFormat.Jpeg);
                    Log.Debug("[HCM][Locker Helper][Capture Photo]" + "[Photo taken File : " + photofileName + "]");
                }
                else
                {
                    Log.Debug("[HCM][Locker Helper][Capture Photo]" + "[Cannot take picture if the camera isn't capturing image!]");
                }

                var captureResponse = new CaptureDto(model.TransactionId, model.LockerId, "jpeg", imageBytes);

                // End using camera
                capture.Release();
                return captureResponse;
            }
        }
        private static byte[] ToByteArray(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Test comport in list. 
        /// </summary>
        public static ComPortsHealthCheck ComPortTest(AppSettings lockerConfiguration)
        {
            try
            {
                var comPorts = new ComPortsHealthCheck();
                if (lockerConfiguration == null) return comPorts;
                comPorts.IsLockPortAvailable = Check(lockerConfiguration.Microcontroller.LockControl);
                comPorts.IsDetectionPortAvailable = Check(lockerConfiguration.Microcontroller.ObjectDetection);
                comPorts.IsScannernPortAvailable = Check(lockerConfiguration.Microcontroller.Scanner);

                Log.Debug("[HCM][Locker Helper][Com Port Test]" + "[Com Port Health : " + JsonSerializer.Serialize(comPorts) + "]");
                return comPorts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool Check(SerialInterface portInterface)
        {
            bool flag = false;
            var spService = new SerialPortBaseService();
            if (spService.IsPortPresented(portInterface.Port))
            {
                flag = !spService.IsPortOpened(portInterface);
            }
            return flag;
        }

        /// <summary>
        /// Check Valid Path
        /// </summary>
        /// <returns>
        ///  True if the caller has the required permissions and path contains the name of an existing file; otherwise, false.
        /// </returns>
        private static bool IsValidPath(string path)
        {
            bool exists = File.Exists(path);
            return exists;
        }

        /// <summary>
        /// Check Valid Path
        /// </summary>
        /// <returns>
        ///  True if the caller has the required permissions and path contains the name of an existing file; otherwise, false.
        /// </returns>
        public static AppSettings GetConfiguration(string path)
        {
            AppSettings configuration = null;
            if (IsValidPath(path))
            {
                var content = File.ReadAllText(path);
                if (string.IsNullOrEmpty(content)) return configuration;
                configuration = JsonSerializer.Deserialize<AppSettings>(content);
                Log.Debug("[HCM][Locker Helper][Get Configuration]" + "[Configuration : " + JsonSerializer.Serialize(configuration) + "]");
            }
            return configuration;
        }


    }
}
