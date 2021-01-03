using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    public class LoggerInfo
    {
        public string MinimumLevel { get; set; }
        public string Path { get; set; }
        public string RollingInterval { get; set; }
    }
}
