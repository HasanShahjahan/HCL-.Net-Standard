using System;

namespace LSS.HCM.Core.DataObjects.Settings
{
   // [Serializable]
    public class AppSettings
    {
        public Microcontroller Microcontroller { get; set; }
        public LockerConfiguration Locker { get; set; }
        public Buzzer Buzzer { get; set; }
        public Socket Socket { get; set; }
        public LoggerInfo Logger { get; set; }
    }
}
