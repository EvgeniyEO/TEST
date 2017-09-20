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
        UdpClient client = null;
        bool stop = true;
        IPEndPoint ipEndPoint;

        static public IPEndPoint ipEndPoint_MUD = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50001);
        static public IPEndPoint ipEndPoint_NAV = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 50002);

        public bool IsConnect() { return !stop; }

        public UDPClientClass(int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Any, port);
        }

        public void Send(byte[] Data, int Size, IPEndPoint endPoint)
        {
            try
            {
                if (client != null)
                {
                    client.Send(Data, Size, endPoint);
                }
            }
            catch (Exception SendExc)
            {
                LogError.MessageError(SendExc, null, "UDP соединение",true);
            }
            
        }
        public void StartReceiving()
        {
            StopReceiving();

            try
            {
                if (client == null)
                {
                    client = new UdpClient(ipEndPoint);
                    
                    if (client != null)
                    {
                        stop = false;
                        Receive(); // initial start of our "loop"
                    }
                }
            }
            catch (Exception StartReceivingExc)
            {
                string Message = null;
                if (StartReceivingExc.HResult == -2147467259)
                    Message = "Выбранный порт: " + ipEndPoint.Port.ToString() + " занят";

                LogError.MessageError(StartReceivingExc, Message, "UDP соединение", true);
            }
        }

        public void StartReceiving(int port)
        {
            StopReceiving();

            try
            {
                stop = false;
                ipEndPoint = new IPEndPoint(IPAddress.Any, port);
                if (client == null)
                {
                    client = new UdpClient(ipEndPoint);

                    if (client != null)
                    {
                        stop = false; 
                        Receive(); // initial start of our "loop"
                    }
                }
            }
            catch (Exception StartReceivingExc)
            {
                string Message = null;
                if (StartReceivingExc.HResult == -2147467259)
                    Message = "Выбранный порт: " + ipEndPoint.Port.ToString() + " занят";

                LogError.MessageError(StartReceivingExc, Message, "UDP соединение", true);
            }
        }

        public void StopReceiving()
        {
            try
            {
                stop = true;
                if (client != null)
                {
                    client.Client.Close();
                    client = null;
                }  
            }
            catch (Exception StopReceivingExc)
            {
                LogError.MessageError(StopReceivingExc, null, "UDP соединение", true);
            }
 
        }

        private void Receive()
        {
            try
            {
                client.BeginReceive(new AsyncCallback(MyReceiveCallback), null);
            }
            catch (Exception ReceiveExc)
            {
                LogError.MessageError(ReceiveExc, null, "UDP соединение", true);
            }
            
        }

        private void MyReceiveCallback(IAsyncResult result)
        {
            try
            {
                if (client != null)
                {
                    IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);

                    UdpClientEventArgs clientArgs = new UdpClientEventArgs();
                    clientArgs.Data = client.EndReceive(result, ref ip);
                    clientArgs.Size = clientArgs.Data.Length;
                    clientArgs.endPoint = ip;
                    OnReceiveData(clientArgs);
                }
                
                if (!stop)
                {
                    Receive(); // <-- this will be our loop
                }
            }
            catch (Exception MyReceiveCallbackExc)
            {
                // Удаленный хост принудительно разорвал существующее подключение
                if (MyReceiveCallbackExc.HResult == -2147467259)
                {
                    LogError.MessageError(MyReceiveCallbackExc, null, "UDP соединение", false);
                    return;
                }
                StopReceiving();
                LogError.MessageError(MyReceiveCallbackExc, null, "UDP соединение", true);
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
