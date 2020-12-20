using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Core.InputOutpuPorts
{

    /// <summary>
    ///   Represents Serial Port Initialization class.
    ///</summary>
    public class SerialPortResource
    {
        /// <summary>
        ///   Initialization information for locker configuration including Microcontroller board, Serial port and Communication port.
        ///</summary>
        /// Parameters.
        /// <param name="portName"> The port to use (for example, COM1).</param>
        /// <param name="baudrate">The baud rate.</param>
        /// <param name="dataBits"> The data bits value.</param>
        /// <param name="readTimeout"> Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish.</param>
        /// <param name="writeTimeout">Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish.</param>
        public SerialPortResource(string portName, int baudrate, int dataBits, int readTimeout, int writeTimeout)
        {
            PortName = portName;
            Baudrate = baudrate;
            DataBits = dataBits;
            ReadTimeout = readTimeout;
            WriteTimeout = writeTimeout;
        }

        /// <summary>
        ///     Gets or sets the serial port name.
        /// </summary>
        /// <returns>
        ///     The Port Name .
        ///</returns>
        public string PortName { get; set; }

        /// <summary>
        ///     Gets or sets the serial baud rate.
        /// </summary>
        /// <returns>
        ///     The baud rate
        ///</returns>
        public int Baudrate { get; set; }

        /// <summary>
        ///      Gets or sets the serial data bits.
        /// </summary>
        /// <returns>
        ///     The Data Bits
        ///</returns>
        public int DataBits { get; set; }

        /// <summary>
        ///     Gets or sets the serial Read Timeout.
        /// </summary>
        /// <returns>
        ///     The Read Timeout
        ///</returns>
        public int ReadTimeout { get; set; }

        /// <summary>
        ///     Gets or sets the serial Write Timeout.
        /// </summary>
        /// <returns>
        ///     The Write Timeout
        ///</returns>
        public int WriteTimeout { get; set; }

    }
    
}
