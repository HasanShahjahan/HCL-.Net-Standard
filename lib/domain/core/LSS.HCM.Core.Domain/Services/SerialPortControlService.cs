using LSS.HCM.Core.Domain.Core.InputOutpuPorts;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Threading;
using LSS.HCM.Core.DataObjects.Settings;

namespace LSS.HCM.Core.Domain.Services
{
    /// <summary>
    ///   Represents actual serial port service of system input and output ports.
    ///</summary>
    public sealed class SerialPortControlService
    {

        /// <summary>
        /// Initialization of serial port control service by seting serial port resouce. 
        /// </summary>
        /// <returns>
        ///  Open compartment actual result with status. 
        /// </returns>
        private readonly SerialPort _serialPort = new SerialPort();
        private string _lockerId = string.Empty;
        private string _brokerTopicEvent = string.Empty;
        private string _socketServer = string.Empty;
        private int _socketPort;
        private SocketClientService client;

        /// <summary>
        /// Initialization of serial port with multiple resources. 
        /// </summary>
        public SerialPortControlService(SerialPortResource serialPortResource)
        {
            _serialPort.PortName = serialPortResource.PortName;
            _serialPort.BaudRate = serialPortResource.Baudrate;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = serialPortResource.DataBits;
            _serialPort.Handshake = Handshake.None;
            _serialPort.ReadTimeout = serialPortResource.ReadTimeout;
            _serialPort.WriteTimeout = serialPortResource.WriteTimeout;

            //Begin();
        }

        /// <summary>
        /// Open serial port.
        /// </summary>
        /// <returns>
        ///  Nothing returned but error throw when its error
        /// </returns>
        public void Begin()
        {
            try
            {
                _serialPort.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Close serial port.
        /// </summary>
        /// <returns>
        ///  Nothing returned but error throw when its error
        /// </returns>
        public void End()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Writes a specified number of bytes to the serial port using data from a buffer.
        /// </summary>
        /// <returns>
        ///  Get list of byte as command response.
        /// </returns>
        public List<byte> Write(List<byte> inputBuffer, int dataLength)
        {
            if (_serialPort.IsOpen)
            {
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
                    catch (Exception)
                    {
                        throw;
                    }
                }
                //_serialPort.Close();
                return commandResponseByte;
            }
            return new List<byte>(); // return empty byte cause of error serialPort
        }

        /// <summary>
        /// Reads a specified number of bytes to the serial port using data from a buffer.
        /// </summary>
        public void Read(object sender, SerialDataReceivedEventArgs e)
        {
            List<byte> responseByte = new List<byte>();
            try
            {
                if (_serialPort.IsOpen)
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        int currentBufferLength = _serialPort.BytesToRead;
                        for (ushort i = 0; i < _serialPort.BytesToRead; i++)
                        {
                            byte byteBuffer = (byte)_serialPort.ReadByte();
                            responseByte.Add(byteBuffer);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads to set locker and name.
        /// </summary>
        /// <returns>
        ///  Get list of byte.
        /// </returns>
        public void SetReadToPublishHandler(AppSettings lockerConfiguration)
        {
            try
            {
                _lockerId = lockerConfiguration.Locker.LockerId;
                _brokerTopicEvent = lockerConfiguration.Mqtt.Topic.Event.Scanner;
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialInputEventHandler);
                _socketServer = "localhost";
                _socketPort = 11000;

                // Socket Scanner
                client = new SocketClientService(_socketServer, _socketPort);
                client.Connect();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Reads and pass data to socket.
        /// </summary>
        private void SerialInputEventHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort _sp = (SerialPort)sender;
                if (_sp.IsOpen)
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        string serialInData = _sp.ReadLine();
                        // Socket Scanner
                        sendDataOnSocket(serialInData);
                        //_serialPort.DiscardInBuffer();
                        
                    }
                }
            }
            catch (Exception) {
                throw;
            }
        }

        // Socket Scanner
        private void sendDataOnSocket(string inputData)
        {
            try
            {
                client.Send(_lockerId + _brokerTopicEvent + "," + inputData);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
