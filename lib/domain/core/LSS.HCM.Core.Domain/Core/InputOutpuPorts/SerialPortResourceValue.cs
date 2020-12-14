using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Core.InputOutpuPorts
{
    public static class SerialPortLockValue
    {
        public const string PortName = "COM1";
        public const int Baudrate = 19200;
        public const int DataBits = 8;
        public const int ReadTimeout = 500;
        public const int WriteTimeout = 500;
    }
    public static class SerialPortDetectionValue
    {
        public const string PortName = "COM2";
        public const int Baudrate = 19200;
        public const int DataBits = 8;
        public const int ReadTimeout = 500;
        public const int WriteTimeout = 500;
    }
}
