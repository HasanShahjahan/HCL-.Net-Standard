using System;
using System.Globalization;

namespace LSS.HCM.Core.Entities.Locker
{
    [Serializable]
    public class Image
    {
        public Image() 
        {
            ImageExtension = string.Empty;
            ImageData = null;
        }
        public Image(string imageExtension, byte[] imageData)
        {
            ImageExtension = imageExtension;
            ImageData = imageData;
        }
        public string ImageExtension { get; set; }
        public byte[] ImageData { get; set; }
    }
}
