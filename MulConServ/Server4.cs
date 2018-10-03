using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MulConServ
{
    class Server4
    {
        private static void RequestClient(object args)
        {
            TcpClient client = (TcpClient)args;
            try
            {
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                string s = String.Empty;
           

                while (!(s = reader.ReadLine()).Equals("Exit") || (s == null))
                {
                    Console.WriteLine(" From Client :" + s);
                    writer.WriteLine("From Server :" + s);
                    writer.Flush();
                }

                while (!s.Equals("Exit"))
                {
                    Console.WriteLine("Enter Data :");
                    s = Console.ReadLine();
                    Console.WriteLine();
                    writer.WriteLine(s);
                    writer.Flush();
                }
                reader.Close();
                writer.Close();
                client.Close();
                Console.WriteLine("Closing Client Connection");
            }
            catch (IOException e)
            {
                Console.WriteLine("Problem with client.... " + e);
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }
        static void Main(string[] args)
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 800);
                listener.Start();
                Console.WriteLine("Server Started.....");
                while (true)
                {
                    Console.WriteLine("Waiting for Connections....");
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Accept new Connection...");
                    Thread t = new Thread(RequestClient);
                    t.Start(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }
    }
}
