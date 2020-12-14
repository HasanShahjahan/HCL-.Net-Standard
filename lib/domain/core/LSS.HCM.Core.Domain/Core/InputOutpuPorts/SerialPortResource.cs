using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Core.InputOutpuPorts
{
    public class SerialPortResource
    {
        public SerialPortResource()
        {
            PortName = SerialPortLockValue.PortName;
            Baudrate = SerialPortLockValue.Baudrate;
            DataBits = SerialPortLockValue.DataBits;
            ReadTimeout = SerialPortLockValue.ReadTimeout;
            WriteTimeout = SerialPortLockValue.WriteTimeout;
        }
        public SerialPortResource(string portName, int baudrate, int dataBits, int readTimeout, int writeTimeout)
        {
            PortName = portName;
            Baudrate = baudrate;
            DataBits = dataBits;
            ReadTimeout = readTimeout;
            WriteTimeout = writeTimeout;
        }
        public string PortName { get; set; }
        public int Baudrate { get; set; }
        public int DataBits { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }

    }
    
}
