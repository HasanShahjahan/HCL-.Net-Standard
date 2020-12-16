using System;

namespace LSS.HCM.Core.DataObjects.Settings
{
    [Serializable]
    public class AppSettings
    {
        public MessageQueuingTelemetryTransport Mqtt { get; set; }
        public Microcontroller Microcontroller { get; set; }
        public LockerConfiguration Locker { get; set; } 
    }
}
