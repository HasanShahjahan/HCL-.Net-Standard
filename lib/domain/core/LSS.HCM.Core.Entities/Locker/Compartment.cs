using System;

namespace LSS.HCM.Core.Entities.Locker
{
    [Serializable]
    public class Compartment
    {
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
        
        public string LockerId { get; set; }

        public string CompartmentId { get; set; }

        public string CompartmentSize { get; set; }

        public bool CompartmentDoorOpen { get; set; }

        public bool CompartmentDoorAvailable { get; set; }

        public bool ObjectDetected { get; set; }

        public string StatusLed { get; set; }
    }
}
