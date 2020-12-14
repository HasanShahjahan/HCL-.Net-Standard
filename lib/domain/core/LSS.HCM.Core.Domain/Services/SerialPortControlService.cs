using LSS.HCM.Core.Domain.Core.InputOutpuPorts;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace LSS.HCM.Core.Domain.Services
{
    public sealed class SerialPortControlService
    {
        private readonly SerialPort _serialPort = new SerialPort();
        public SerialPortControlService()
        {
        }
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
