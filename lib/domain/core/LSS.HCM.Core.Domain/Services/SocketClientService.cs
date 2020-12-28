using Serilog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LSS.HCM.Core.Domain.Services
{
    class SocketClientService
    {
        private Socket _client;
        private IPHostEntry _host;
        private IPAddress _ipAddress;
        private IPEndPoint _remoteEP;

        public SocketClientService(string hostName, int hostPort)
        {
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

                Log.Debug("[HCM][Socket Client Service][Connect]" + "[Socket connected to {0} : " + _client.RemoteEndPoint.ToString() + "]");
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
                    Log.Debug("[HCM][Socket Client Service][Send]" + "[Send normally][message : " + sendMsg + "]");
                }
            }
            catch (SocketException)
            {
                _client.Close();
                Connect();
                Send(sendMsg);
                Log.Debug("[HCM][Socket Client Service][Send]" + "[Try send when some error][message : " + sendMsg + "]");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}