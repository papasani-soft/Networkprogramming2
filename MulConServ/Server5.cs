using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MulConServ
{
    class Server5
    {
        private static void RequestClient(object args)    
        {
            TcpClient client = (TcpClient)args;
            try
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                Console.WriteLine("\n Enter msg for client  or enter <exit> for close else enter msg  \n");
                 string msg = Console.ReadLine();
             //   client.Send(asen.GetBytes(msg));
                Console.WriteLine("\nSent msg  ");   
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            Console.ReadLine();
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
