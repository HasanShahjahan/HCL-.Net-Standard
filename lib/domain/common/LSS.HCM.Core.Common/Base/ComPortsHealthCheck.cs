using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Common.Base
{
    public class ComPortsHealthCheck
    {
        public bool IsLockPortAvailable { get; set; }
        public bool IsDetectionPortAvailable { get; set; }
        public bool IsScannernPortAvailable { get; set; }
    }
}
