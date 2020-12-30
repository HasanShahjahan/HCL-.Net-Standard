using LSS.HCM.Core.Domain.Core.InputOutpuPorts;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Threading;
using LSS.HCM.Core.DataObjects.Settings;
using System.Linq;

namespace LSS.HCM.Core.Domain.Services
{
    /// <summary>
    ///   Represents actual serial port service of system input and output ports.
    ///</summary>
    public sealed class SerialPortBaseService
    {
        /// <summary>
        /// General constructure for using all get function. 
        /// </summary>
        public SerialPortBaseService()
        {

        }

        /// <summary>
        /// Open serial port.
        /// </summary>
        /// <returns>
        ///  Nothing returned but error throw when its error
        /// </returns>
        public string[] GetComPortNames()
        {
            try
            {
                 return SerialPort.GetPortNames();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Test serial port available.
        /// </summary>
        /// <returns>
        ///  Nothing returned but error throw when its error
        /// </returns>
        public bool IsPortOpened(SerialInterface controlModuleObj)
        {
            try
            {
                var controlModule = new SerialPortControlService(new SerialPortResource(controlModuleObj.Port, controlModuleObj.Baudrate, controlModuleObj.DataBits, 500, 500));
                return controlModule.IsOpen();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Test serial port available.
        /// </summary>
        /// <returns>
        ///  Nothing returned but error throw when its error
        /// </returns>
        public bool IsPortPresented(string portName)
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                return ports.Any(x => x == portName);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
