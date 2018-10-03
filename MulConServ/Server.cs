using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MulConServ
{
    class Server
    {
        static void Main(string[] args)
        {
            int port = 13000;
            string IpAddress = "127.0.0.1";
            Socket ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress),port);
            ServerListener.Bind(ep);
            ServerListener.Listen(200);
            Console.WriteLine("Server is Listening.........");
            Socket ClientSocket = default(Socket);
            int counter = 0;
            Server s = new Server();
            while (true)
            {
                counter++;
                ClientSocket = ServerListener.Accept();
                Console.WriteLine(counter+" clients connected");
                Thread UserThread = new Thread(new ThreadStart(()=>s.User(ClientSocket)));
                UserThread.Start();
            }
        }
        public void User(Socket Client)
        {
            while(true)
            {
                byte[] msg = new byte[1024];
                int size = Client.Receive(msg);
                string dataReceived = Encoding.ASCII.GetString(msg, 0, size);
                Console.WriteLine("Received : " + dataReceived);
                Console.WriteLine("Sending back : " + dataReceived);
                Client.Send(msg, 0,size, SocketFlags.None);
               
            }  
        }
    }
}
