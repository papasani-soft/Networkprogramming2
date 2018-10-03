using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MulConServ
{
    class Server3
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse("192.168.1.22");
                TcpListener myList = new TcpListener(ipAd, 8001);
                myList.Start();
                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine("The local End point is  :" + myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");
                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                string msg = "";
                //  Console.WriteLine("enter <exit> for close else enter msg ");
                int i = 0;
                while (!msg.Trim().Equals("exit"))
                {
                    msg = "";
                    byte[] b = new byte[1000];
                    int k = s.Receive(b);
                    Console.WriteLine("Recieved...");
                    for (i = 0; i < k; i++)
                    {
                        Console.Write(Convert.ToChar(b[i]));
                    }
                    ASCIIEncoding asen = new ASCIIEncoding();
                    Console.WriteLine("\n Enter msg for client  or enter <exit> for close else enter msg  \n");
                    msg = Console.ReadLine();
                    s.Send(asen.GetBytes(msg));
                    Console.WriteLine("\nSent msg  ");

                }
                s.Close();
                myList.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            Console.ReadLine();
        }

    }

}
