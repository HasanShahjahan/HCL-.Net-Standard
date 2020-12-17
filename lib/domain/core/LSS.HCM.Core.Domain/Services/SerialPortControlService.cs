using LSS.HCM.Core.Domain.Core.InputOutpuPorts;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace LSS.HCM.Core.Domain.Services
{
    /// <summary>
    ///   Represents actual serial port service of system input and output ports.
    ///</summary>
    public sealed class SerialPortControlService
    {
        /// <summary>
        ///   Initialization of serial port of system input/output.
        ///</summary>
        private readonly SerialPort _serialPort = new SerialPort();

        /// <summary>
        /// Initialization of serial port control service by seting serial port resouce. 
        /// </summary>
        /// <returns>
        ///  Open compartment actual result with status. 
        /// </returns>
        public SerialPortControlService(SerialPortResource serialPortResource)
        {
            _serialPort.PortName = serialPortResource.PortName;
            _serialPort.BaudRate = serialPortResource.Baudrate;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = serialPortResource.DataBits;
            _serialPort.Handshake = Handshake.None;
            _serialPort.ReadTimeout = serialPortResource.ReadTimeout;
            _serialPort.WriteTimeout = serialPortResource.WriteTimeout;
        }

        /// <summary>
        /// Writes a specified number of bytes to the serial port using data from a buffer.
        /// </summary>
        /// <returns>
        ///  Get list of byte as command response.
        /// </returns>
        public List<byte> Write(List<byte> inputBuffer, int dataLength)
        {
            _serialPort.Open();
            List<byte> commandResponseByte = new List<byte>();
            _serialPort.Write(inputBuffer.ToArray(), 0, inputBuffer.Count);
            bool _continue = true;

            while (_continue)
            {
                try
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        int currentBufferLength = _serialPort.BytesToRead;
                        for (ushort i = 0; i < _serialPort.BytesToRead; i++)
                        {
                            byte byteBuffer = (byte)_serialPort.ReadByte();
                            commandResponseByte.Add(byteBuffer);
                        }
                        if (commandResponseByte.Count < dataLength)
                        {
                            _continue = true;
                        }
                        else
                        {
                            _continue = false;
                        }
                    }
                }
                catch (TimeoutException) { }
            }
            _serialPort.Close();
            return commandResponseByte;
        }
    }
}
