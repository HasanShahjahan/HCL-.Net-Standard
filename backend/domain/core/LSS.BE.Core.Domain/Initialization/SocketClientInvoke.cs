using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LSS.BE.Core.Domain.Initialization
{
    public class SocketClientInvoke
    {
        private Socket _client;
        private IPHostEntry _host;
        private IPAddress _ipAddress;
        private IPEndPoint _remoteEP;

        public SocketClientInvoke(string hostName, int hostPort)
        {
            if (string.IsNullOrEmpty(hostName)) hostName = "localhost";
            if (hostPort == 0) hostPort = 11000;

            _host = Dns.GetHostEntry(hostName);
            _ipAddress = _host.AddressList[0];
            _remoteEP = new IPEndPoint(_ipAddress, hostPort);
        }
        public void Connect()
        {
            try
            {
                _client = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _client.Connect(_remoteEP);

                Console.WriteLine("Socket connected to {0}", _client.RemoteEndPoint.ToString());
            }
            catch (Exception)
            {
                //throw;
            }
        }
        public void Send(string sendMsg)
        {
            try
            {
                int bytesSent = _client.Send(Encoding.ASCII.GetBytes(sendMsg));
                if (bytesSent == 0)
                {
                    Connect();
                    Send(sendMsg);
                }
            }
            catch (SocketException)
            {
                _client.Close();
                Connect();
                Send(sendMsg);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
