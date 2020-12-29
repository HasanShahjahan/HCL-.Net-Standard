using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LSS.BE.Core.Domain.Initialization
{
    /// <summary>
    ///   Represents the socket client and it's implementation.
    ///</summary>
    public class SocketClientInvoke
    {
        /// <summary>
        ///   Initialize socket client.
        ///</summary>
        private Socket _client;

        /// <summary>
        ///   Initialize socket host.
        ///</summary>
        private IPHostEntry _host;

        /// <summary>
        ///   Initialize socket Ip Address.
        ///</summary>
        private IPAddress _ipAddress;

        /// <summary>
        ///   Initialize socket end point.
        ///</summary>
        private IPEndPoint _remoteEP;

        /// <summary>
        ///   Initialization information for socket server by host and port.
        ///</summary>
        public SocketClientInvoke(string hostName, int hostPort)
        {
            if (string.IsNullOrEmpty(hostName)) hostName = "localhost";
            if (hostPort == 0) hostPort = 11000;

            _host = Dns.GetHostEntry(hostName);
            _ipAddress = _host.AddressList[0];
            _remoteEP = new IPEndPoint(_ipAddress, hostPort);
        }

        /// <summary>
        /// Connet with socket by address family, socket type and protocol type.
        /// </summary>
        /// <returns>
        ///  Gets the result of socket connection. 
        /// </returns>
        public void Connect()
        {
            try
            {
                _client = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _client.Connect(_remoteEP);
                Log.Information("[Socket Client Invoke][Socket connected]" + "[" + _client.RemoteEndPoint.ToString() + "]");
            }
            catch (Exception ex)
            {
                Log.Error("[Socket Client Invoke][Connect]" + "[" + ex + "]");
            }
        }

        /// <summary>
        /// Sends message and connection close.
        /// </summary>
        /// <returns>
        ///  Send messages.
        /// </returns>
        public void Send(string sendMsg)
        {
            try
            {
                int bytesSent = _client.Send(Encoding.ASCII.GetBytes(sendMsg));
                if (bytesSent == 0)
                {
                    Connect();
                    Send(sendMsg);
                    Log.Information("[Socket Client Invoke][Send]" + "[" + sendMsg + "]");
                }
            }
            catch (SocketException)
            {
                _client.Close();
                Connect();
                Send(sendMsg);
            }
            catch (Exception ex)
            {
                Log.Error("[Socket Client Invoke][Send]" + "[" + ex + "]");
            }
        }
    }
}
