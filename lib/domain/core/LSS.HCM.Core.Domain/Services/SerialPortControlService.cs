using LSS.HCM.Core.Domain.Core.InputOutpuPorts;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Common.Utiles;
using Serilog;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace LSS.HCM.Core.Domain.Services
{
    /// <summary>
    ///   Represents actual serial port service of system input and output ports.
    ///</summary>
    public class SerialPortControlService
    {
        /// <summary>
        ///   To detect redundant calls.
        ///</summary>
        private bool _disposed = false;

        /// <summary>
        ///   Instantiate a SafeHandle instance.
        ///</summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Initialization of serial port control service by seting serial port resouce. 
        /// </summary>
        /// <returns>
        ///  Open compartment actual result with status. 
        /// </returns>
        private readonly SerialPort _serialPort = new SerialPort();
        private string _SerialDataEventName;
        private Func<string, string> _SerialDataProcessing;

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
                Log.Debug("[HCM][SerialPort Control Service][Begin]" + "[Port : " + _serialPort.PortName + "]" + "[Baud Rate : " + _serialPort.BaudRate + "]");
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
                    Log.Debug("[HCM][SerialPort Control Service][End]" + "[Port : " + _serialPort.PortName + "]");
                }
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
        public bool IsOpen()
        {
            try
            {
                return _serialPort.IsOpen;
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
                try
                {
                    _serialPort.Write(inputBuffer.ToArray(), 0, inputBuffer.Count);
                }
                catch (Exception)
                {
                    throw;
                }
                Log.Debug("[HCM][SerialPort Control Service][Write]" + "[Port : " + _serialPort.PortName + "]" + "[Input Buffer : " + inputBuffer + "]");

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
                        Log.Debug("[HCM][SerialPort Control Service][Read]" + "[Port : " + _serialPort.PortName + "]" + "[Buffer : " + responseByte + "]");

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
        public void SetReadToPublishHandler(SerialInterface controllerModule, Func<string,string> dataProcessingFunc)
        {
            try
            {
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialInputEventHandler);
                _SerialDataProcessing = dataProcessingFunc;
                _SerialDataEventName = controllerModule.Name;
                Log.Debug("[HCM][SerialPort Control Service][Set Read Handler]" + "[Port : " + _serialPort.PortName + "]" + "[Event Name : " + _SerialDataEventName + "]");
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
                        var dictData = new Dictionary<string, string>();
                        dictData.Add("eventname", _SerialDataEventName);
                        dictData.Add("data", serialInData);
                        
                        string jsonString = Utiles.DictToJson(dictData);
                        _SerialDataProcessing(jsonString);
                        Log.Debug("[HCM][SerialPort Control Service][Read Handler]" + "[Port : " + _serialPort.PortName + "]" + "[Event Name : " + _SerialDataEventName + "]" + "[Read Buffer : " + serialInData + "]");

                    }
                }
            }
            catch (Exception) {
                throw;
            }
        }

        /// <summary>
        ///   Public implementation of Dispose pattern callable by consumers.
        ///</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Protected implementation of Dispose pattern.
        ///</summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
                _serialPort?.Dispose();
            }

            _disposed = true;
        }
    }
}
