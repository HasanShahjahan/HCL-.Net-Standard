using System;

namespace LSS.HCM.Core.Entities.Locker
{
    /// <summary>
    ///   Represents compartment as a sequence of compartment entity.
    ///</summary>
    
    [Serializable]
    public class Compartment
    {
        /// <summary>
        ///     Initializes a new instance of the Compartment class to the value indicated
        ///     by all members.
        /// </summary>
        public Compartment() 
        {
            LockerId = string.Empty;
            CompartmentId = string.Empty;
            CompartmentSize = string.Empty;
            CompartmentDoorOpen = false;
            CompartmentDoorAvailable = false;
            ObjectDetected = false;
            StatusLed = string.Empty;

        }

        /// <summary>
        ///    Initializes a new instance of the Compartment class to the value indicated by all members.
        /// </summary>
        /// Parameters.
        /// <param name="compartmentId"> An entity Compartment Id.</param>
        /// <param name="lockerId">An entity Locker Id </param>
        /// <param name="compartmentSize"> An entity Compartment size e.g small, medium or large.</param>
        /// <param name="compartmentDoorOpen">Boolen flag for compartment door or close.</param>
        /// <param name="compartmentDoorAvailable">This flag indicated if compartment door has any faulty status then this flag will notify whether available or not.</param>
        /// <param name="objectDetected">Boolen object detection status whether any object inside or not.</param>
        /// <param name="statusLed">LED status with ON, OFF or BLINKING</param>
        public Compartment(string lockerId, string compartmentId, string compartmentSize, bool compartmentDoorOpen, bool compartmentDoorAvailable, bool objectDetected, string statusLed)
        {
            LockerId = lockerId;
            CompartmentId = compartmentId;
            CompartmentSize = compartmentSize;
            CompartmentDoorOpen = compartmentDoorOpen;
            CompartmentDoorAvailable = compartmentDoorAvailable;
            ObjectDetected = objectDetected;
            StatusLed = statusLed;
        }

        /// <summary>
        ///     Gets and sets the Locker Id in the current Compartment entity.
        /// </summary>
        /// <returns>
        ///     The Locker Id in the current Compartment.
        ///</returns>
        
        public string LockerId { get; set; }

        /// <summary>
        ///     Gets and sets the Compartment Id in the current Compartment entity.
        /// </summary>
        /// <returns>
        ///     The Compartment Id in the current Compartment.
        ///</returns>

        public string CompartmentId { get; set; }

        /// <summary>
        ///     Gets and sets the Compartment Size in the current Compartment entity.
        /// </summary>
        /// <returns>
        ///     The Compartment Size in the current Compartment.
        ///</returns>

        public string CompartmentSize { get; set; }

        /// <summary>
        ///     Gets and sets the Compartment Door Open in the current Compartment entity.
        /// </summary>
        /// <returns>
        ///     The Compartment Door Open status in the current Compartment.
        ///</returns>

        public bool CompartmentDoorOpen { get; set; }

        /// <summary>
        ///     Gets and sets the Compartment Door Available in the current Compartment entity.
        /// </summary>
        /// <returns>
        ///     The Compartment Door Available status in the current Compartment.
        ///</returns>

        public bool CompartmentDoorAvailable { get; set; }

        /// <summary>
        ///     Gets and sets the Object Detected in the current Compartment entity.
        /// </summary>
        /// <returns>
        ///     The Object Detected status in the current Compartment.
        ///</returns>

        public bool ObjectDetected { get; set; }


        /// <summary>
        ///     Gets and sets the LED Status in the current Compartment entity.
        /// </summary>
        /// <returns>
        ///     The LED Status in the current Compartment.
        ///</returns>
        public string StatusLed { get; set; }
    }
}
