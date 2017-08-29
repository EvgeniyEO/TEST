using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TestMAP
{
    class UDPClientClass
    {
        UdpClient client;
        bool stop;
        IPEndPoint ipEndPoint;

        public UDPClientClass(int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Any, port);

        }

        public void Send(byte[] Data, int Size, IPEndPoint endPoint)
        {
            client.Send(Data, Size, endPoint);
        }
        public void StartReceiving()
        {
            client = new UdpClient(ipEndPoint);
            Receive(); // initial start of our "loop"
        }

        public void StopReceiving()
        {
            stop = true;
            client.Client.Close();
        }

        private void Receive()
        {
            client.BeginReceive(new AsyncCallback(MyReceiveCallback), null);
        }

        private void MyReceiveCallback(IAsyncResult result)
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);

            UdpClientEventArgs clientArgs = new UdpClientEventArgs();
            clientArgs.Data = client.EndReceive(result, ref ip);
            clientArgs.Size = clientArgs.Data.Length;
            clientArgs.endPoint = ip;
            OnReceiveData(clientArgs);

            if (!stop)
            {
                Receive(); // <-- this will be our loop
            }
        }

        protected virtual void OnReceiveData(UdpClientEventArgs e)
        {
            EventHandler<UdpClientEventArgs> handler = ReceiveData;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<UdpClientEventArgs> ReceiveData;

        public class UdpClientEventArgs : EventArgs
        {
            public byte[] Data { get; set; }
            public int Size { get; set; }
            public IPEndPoint endPoint { get; set; }
        }
    }
}
