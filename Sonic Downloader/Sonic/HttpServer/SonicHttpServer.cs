using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sonic.HttpServer
{
    public class SonicHttpServer
    {
        private  TcpListener tcpListener;

        /// <summary>
        /// Whether or Not Server is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        private IPAddress iPAddress=IPAddress.Any;
        /// <summary>
        /// IP Address where server is listening on, Default(IPAddress.Any).
        /// </summary>
        public IPAddress IPAddress
        {
            get
            {
                return iPAddress;
            }
            set
            {
                iPAddress = value;
                Initialize();
            }
        }

        private int port = 5555;
        /// <summary>
        /// Port to Open, default(5555).
        /// </summary>
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
                Initialize();
            }
        }


        /// <summary>
        /// Allots Default IP to IPAddress.Any And Port to 5555.
        /// </summary>
        public SonicHttpServer()
        {
            Initialize();
        }

        /// <summary>
        /// Assign Port and Allots IP as IPAddress.Any.
        /// </summary>
        /// <param name="port">Port To Open</param>
        public SonicHttpServer(int port)
        {
            this.port = port;
            Initialize();
        }

        /// <summary>
        /// Assign Port and Allots IP as IPAddress.Any.
        /// </summary>
        /// <param name="ip">IP To listen for.</param>
        /// <param name="port">Port To Open.</param>
        public SonicHttpServer(IPAddress ip,int port)
        {
            this.port = port;
            iPAddress = ip;
            Initialize();
        }

        /// <summary>
        /// Assign Port and Allots IP as IPAddress.Any.
        /// </summary>
        /// <param name="ip">IP To listen for.</param>
        /// <param name="port">Port To Open.</param>
        public SonicHttpServer(string ip, int port)
        {
            this.port = port;
            iPAddress = IPAddress.Parse(ip);
            Initialize();
        }

        ~SonicHttpServer()
        {
            if(IsRunning)
            tcpListener?.Stop();
        }

        private void Initialize()
        {
            tcpListener = new TcpListener(IPAddress, Port);
        }

        /// <summary>
        /// Start server on alloted port and IP.
        /// </summary>
        public void Start()
        {
            IsRunning = true;
            tcpListener.Start();
            ClientReciever();
        }

        /// <summary>
        /// Start server on alloted port and IP with limitation on no of connections.
        /// </summary>
        /// <param name="backlog">No. of Max Connections.</param>
        public void Start(int backlog)
        {
            IsRunning = true;
            tcpListener.Start(backlog);
            ClientReciever();
        }

        private async void ClientReciever()
        {
       
            while(IsRunning)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                if (IsRunning)
                   await Task.Run(()=>ClientHandler(client));
                else
                {
                    tcpListener.Stop();
                }
            }
        }

        private void ClientHandler(TcpClient client)
        {
            ConnectionEventArgs connectionEvent = new ConnectionEventArgs(client);

            if(connectionEvent.Request.URL == null)
            {
                connectionEvent.Response.Close();
                return;
            }

            OnConnection?.Invoke(this,connectionEvent);
        }

        /// <summary>
        /// Stop Server from running further.
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
        }


        public delegate void ConnectionHandler(object sender, ConnectionEventArgs e);

        /// <summary>
        /// Provide Request and Response object on connection.
        /// </summary>
        public event ConnectionHandler OnConnection;

       
    }
}
namespace Sonic.HttpServer
{
    public class ConnectionEventArgs:EventArgs
    {
        private  TcpClient client;
        private NetworkStream networkStream;

        public ConnectionEventArgs(TcpClient client)
        {
            this.client = client;
            networkStream = client.GetStream();
            Request = new SonicRequest( client, networkStream);
            Response = new SonicResponse( client, networkStream);
            User =(IPEndPoint)client.Client.RemoteEndPoint;
        }
        public Socket GetSocket()
        {
            return client.Client;
        }

        public IPEndPoint User { get; }
        public SonicRequest Request { get; }
        public SonicResponse Response { get; }
    }

}
