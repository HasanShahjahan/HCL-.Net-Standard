using System.Globalization;

namespace LSS.HCM.Core.Entities.Locker
{
    public class Capture : ObjectBase
    {
        public Capture() 
        {
            LockerId = string.Empty;
            CaptureImage = new Image();
        }
        public Capture(string lockerId, Image captureImage)
        {
            LockerId = lockerId;
            CaptureImage = captureImage;
        }
        public Capture(string lockerId, string imageExtension, byte[] imageData)
        {
            LockerId = lockerId;
            CaptureImage = new Image(imageExtension, imageData);
        }
        public string LockerId { get; set; }
        public Image  CaptureImage{ get; set; }
    }
}
