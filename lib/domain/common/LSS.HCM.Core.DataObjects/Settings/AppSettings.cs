using System;

namespace LSS.HCM.Core.DataObjects.Settings
{
    [Serializable]
    public class AppSettings
    {
        public Buzzer Buzzer { get; set; }
        public MessageQueuingTelemetryTransport Mqtt { get; set; }
        public Microcontroller Microcontroller { get; set; }
        public LockerConfiguration Locker { get; set; } 
    }
}
