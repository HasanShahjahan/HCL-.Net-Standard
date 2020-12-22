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
        private  string _lockerId = string.Empty;
        private  string _serialPortName = string.Empty;
        private  string _mqttServer = string.Empty;
        private  string _brokerTopicEvent = string.Empty;

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

            Begin();
        }

        /// <summary>
        /// Open serial port.
        /// </summary>
        /// <returns>
        ///  Get boolen result of serial port open or not.
        /// </returns>
        public bool Begin()
        {
            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SerialportError: " + ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Close serial port.
        /// </summary>
        /// <returns>
        ///  Get boolen result of serial port close or not.
        /// </returns>
        public bool End()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                    return true;
                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SerialportError: " + ex.ToString());
                return false;
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
                    catch (TimeoutException) { }
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
            catch (TimeoutException) { }
        }

        /// <summary>
        /// Reads to set locker and name.
        /// </summary>
        /// <returns>
        ///  Get list of byte.
        /// </returns>
        public List<byte> SetReadToPublishHandler(AppSettings lockerConfiguration)
        {
            List<byte> responseByte = new List<byte>();
            try
            {
                _lockerId = lockerConfiguration.Locker.LockerId;
                _serialPortName = lockerConfiguration.Microcontroller.Scanner.Name;
                _mqttServer = lockerConfiguration.Mqtt.Server + ":" + lockerConfiguration.Mqtt.Port + "/mqtt";
                _brokerTopicEvent = lockerConfiguration.Mqtt.Topic.Event.Scanner;
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(ReadToPublish);
            }
            catch (TimeoutException) { }

            return responseByte;
        }

        /// <summary>
        /// Reads to publish to the MQTT broker.
        /// </summary>
        public void ReadToPublish(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort _sp = (SerialPort)sender;
                if (_sp.IsOpen)
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        string indata = _sp.ReadLine();
                        Console.WriteLine("Data Received:");
                        Console.Write(indata);

                        // Create a new MQTT client.
                        MqttFactory factory = new MqttFactory();
                        var mqttClient = factory.CreateMqttClient();

                        var options = new MqttClientOptionsBuilder()
                            .WithWebSocketServer(_mqttServer)
                            .Build();
                        mqttClient.ConnectAsync(options, CancellationToken.None);

                        while (!mqttClient.IsConnected) ;
                        mqttClient.PublishAsync(_lockerId + _brokerTopicEvent, indata);
                        _serialPort.DiscardInBuffer();
                    }
                }
            }
            catch (TimeoutException) { }
        }
    }
}
