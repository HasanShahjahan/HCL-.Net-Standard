using Serilog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LSS.HCM.Core.Domain.Services
{
    /// <summary>
    ///   Socket Listener Server wait for message from socketClient on the specified port
    ///</summary>
    public sealed class SocketListenerService
    {
        /// <summary>
        ///   Global variable for SocketListenerService.
        ///</summary>
        private Socket _listener;
        private IPHostEntry _host;
        private IPAddress _ipAddress;
        private IPEndPoint _localEndPoint;
        private Func<string, string> _dataProcess;

        /// <summary>
        ///   Constructor of socket Listener service (Server)
        ///</summary>
        public SocketListenerService(string hostName, int hostPort)
        {
            _host = Dns.GetHostEntry(hostName);
            _ipAddress = _host.AddressList[0];
            _localEndPoint = new IPEndPoint(_ipAddress, hostPort);
        }

        /// <summary>
        /// Create socket and start listener server
        /// </summary>
        /// <returns>
        ///  nothing
        /// </returns>
        public void AsyncStart(Func<string, string> DataProcessCallback)
        {
            try
            {
                _dataProcess = DataProcessCallback;

                _listener = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _listener.Bind(_localEndPoint);
                _listener.Listen(10);
                _listener.BeginAccept(new AsyncCallback(AcceptCallback), _listener);
                Log.Debug("[HCM][Socket Listener Service][Async Start][Start Listen Socket]");

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Callback when clients acceptted
        /// </summary>
        /// <returns>
        ///  nothing
        /// </returns>
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket ConnectedClient = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = ConnectedClient;
            ConnectedClient.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            Log.Debug("[HCM][Socket Listener Service][Accepted Callback][Client accepted]");
        }

        /// <summary>
        /// Callback when recieved clients message
        /// </summary>
        /// <returns>
        ///  nothing
        /// </returns>
        private void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    state.sb.Clear();
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    String content = String.Empty;
                    content = state.sb.ToString();
                    Log.Debug("[HCM][Socket Listener Service][Read Callback]" + "[ message : " + content + "]");

                    // run data processing callback
                    _dataProcess(content);

                    // Begin Reading Callback again
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }

            }
            catch (SocketException)
            {
                _listener.BeginAccept(new AsyncCallback(AcceptCallback), _listener);
            }
        }
    }
    /// <summary>
    ///   Socket Listener stage object
    ///</summary>
    public class StateObject
    {
        /// <summary>
        ///   Size of receive buffer.  
        ///</summary>
        public const int BufferSize = 1024;

        /// <summary>
        ///   Receive buffer. 
        ///</summary>
        public byte[] buffer = new byte[BufferSize];

        /// <summary>
        ///   Received data string.
        ///</summary>
        public StringBuilder sb = new StringBuilder();

        /// <summary>
        ///   Client socket.
        ///</summary>
        public Socket workSocket = null;
    }
}