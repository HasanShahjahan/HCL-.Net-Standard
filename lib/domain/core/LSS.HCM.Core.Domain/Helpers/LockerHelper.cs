using LSS.HCM.Core.Common.Base;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Settings;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;

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
        public static CaptureDto CapturePhoto(DataObjects.Models.Capture model)
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

                    snapshot.Save(string.Format(@"{0}.jpeg", Guid.NewGuid()), ImageFormat.Jpeg); //This saved real picture to physical location. 
                    imageBytes = ToByteArray(snapshot, ImageFormat.Jpeg);
                }
                else
                {
                    Console.WriteLine("Cannot take picture if the camera isn't capturing image!");
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
        /// Image byte array preparation by open library.
        /// </summary>
        public static ComPortsHealthCheck ComPortInit(AppSettings lockerConfiguration)
        {
            var comPorts = new ComPortsHealthCheck();
            try 
            {
                string[] ports = SerialPort.GetPortNames();
                comPorts.IsScannernPortAvailable = ports.Any(x => x == lockerConfiguration.Microcontroller.Scanner.Port);
                comPorts.IsLockPortAvailable = ports.Any(x => x == lockerConfiguration.Microcontroller.LockControl.Port);
                comPorts.IsDetectionPortAvailable = ports.Any(x => x == lockerConfiguration.Microcontroller.ObjectDetection.Port);
            }
            catch (Exception ex)
            {

            }
            return comPorts;
        }
    }
}
