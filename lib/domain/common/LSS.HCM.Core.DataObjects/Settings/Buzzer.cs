using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    public class Buzzer
    {
        public double Timeout { get; set; }
        public string AudioFileName { get; set; }
        public int PlaybackTime { get; set; }
    }
}
