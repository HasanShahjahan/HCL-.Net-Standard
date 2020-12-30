using System;
using System.Globalization;

namespace LSS.HCM.Core.Entities.Locker
{
    /// <summary>
    ///   Represents capture image object as a sequence of taking photo.
    ///</summary>
    
    [Serializable]
    public class Image
    {
        /// <summary>
        ///   Initialization image for taking photo object.
        ///</summary>
        public Image() 
        {
            ImageExtension = string.Empty;
            ImageData = null;
        }

        /// <summary>
        ///   Initialization image for taking photo object.
        ///</summary>
        public Image(string imageExtension, byte[] imageData)
        {
            ImageExtension = imageExtension;
            ImageData = imageData;
        }

        /// <summary>
        ///     Sets the image extension of object.
        /// </summary>
        /// <returns>
        ///     The image extension.
        ///</returns>
        public string ImageExtension { get; set; }

        /// <summary>
        ///     Sets the image extension of object.
        /// </summary>
        /// <returns>
        ///     The image extension.
        ///</returns>
        public byte[] ImageData { get; set; }
    }
}
