using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MSClient
{
    class Client
    {
        public static void Main(string[] args)
        {
            int port = 13000;
            string IpAddress = "127.0.0.1";
            Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress), port);
            ClientSocket.Connect(ep);
            Console.WriteLine("Client is connected");
            while (true)
            {
                string messageFromClient = null;
                Console.WriteLine("Enter the message");
                messageFromClient = Console.ReadLine();
                ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient),0,messageFromClient.Length,SocketFlags.None);
                byte[] messageFromServer = new byte[1024];
                int size=ClientSocket.Receive(messageFromServer);
                 string data = Encoding.ASCII.GetString(messageFromServer, 0, size);
                Console.WriteLine("server"+data);
             //   Console.WriteLine("server "+System.Text.Encoding.ASCII.GetString(messageFromServer,0,size));
            }
        }      
    }
}
