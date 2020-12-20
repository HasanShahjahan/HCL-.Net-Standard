using System.Globalization;

namespace LSS.HCM.Core.Entities.Locker
{
    /// <summary>
    ///   Represents Image Capture as a sequence of taking photo.
    ///</summary>
    public class Capture : ObjectBase
    {
        /// <summary>
        ///     Initializes a new instance of the Capture Image class to the value indicated
        ///     by all members.
        /// </summary>
        public Capture() 
        {
            LockerId = string.Empty;
            CaptureImage = new Image();
        }

        /// <summary>
        ///    Initializes a new instance of the Capture Image to the value indicated by 
        ///    all members.
        /// </summary>
        /// Parameters.
        /// <param name="lockerId"> An request Transaction Id.</param>
        /// <param name="captureImage">An Capture Image object</param>
        public Capture(string lockerId, Image captureImage)
        {
            LockerId = lockerId;
            CaptureImage = captureImage;
        }

        /// <summary>
        ///    Initializes a new instance of the Capture Image to the value indicated by 
        ///    all members.
        /// </summary>
        /// Parameters.
        /// <param name="lockerId"> An request Transaction Id.</param>
        /// <param name="imageExtension">An image extension of image object</param>
        /// <param name="imageData">An image byte array of image object</param>
        public Capture(string lockerId, string imageExtension, byte[] imageData)
        {
            LockerId = lockerId;
            CaptureImage = new Image(imageExtension, imageData);
        }

        /// <summary>
        ///     Gets and sets the Locker Id in the current Capture Image object.
        /// </summary>
        /// <returns>
        ///     The Locker Id in the current Compartment.
        ///</returns>
        public string LockerId { get; set; }

        /// <summary>
        ///     Gets and sets the Image object in the current Capture object.
        /// </summary>
        /// <returns>
        ///     The Image in the current Compartment.
        ///</returns>
        public Image  CaptureImage{ get; set; }
    }
}
