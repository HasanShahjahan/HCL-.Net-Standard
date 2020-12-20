using LSS.HCM.Core.Domain.Core.InputOutpuPorts;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
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
        private string _SerialPortName = string.Empty;
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

            InitialSerialPort();
        }

        public bool InitialSerialPort()
        {
            try
            {
                _serialPort.Open();
            }
            catch(Exception ex)
            {
                Console.WriteLine("SerialportError: " + ex.ToString());
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Writes a specified number of bytes to the serial port using data from a buffer.
        /// </summary>
        /// <returns>
        ///  Get list of byte as command response.
        /// </returns>
        public List<byte> Write(List<byte> inputBuffer, int dataLength)
        {
            if(_serialPort.IsOpen)
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

        public List<byte> SetReadToPublishHandler(string lockerId, string controllerName)
        {
            List<byte> responseByte = new List<byte>();
            try
            {
                _lockerId = lockerId;
                _SerialPortName = controllerName;
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(ReadToPublish);
            }
            catch (TimeoutException) { }

            //_serialPort.Close();
            return responseByte;
        }
        
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
                            .WithWebSocketServer("localhost:1883/mqtt")
                            .Build();
                        mqttClient.ConnectAsync(options, CancellationToken.None);

                        while (!mqttClient.IsConnected);
                        //if (!mqttClient.IsConnected)
                        //{
                            //mqttClient.ConnectAsync(options, CancellationToken.None);
                            //mqttClient.DisconnectAsync();
                        //}
                        mqttClient.PublishAsync(_lockerId + "/event/" + _SerialPortName, indata);
                        _serialPort.DiscardInBuffer();
                    }
                }
            }
            catch (TimeoutException) { }
        }
    }
}
