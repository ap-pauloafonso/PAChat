using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LibMessages;

namespace Server
{
    class Program
    {

        static void Main(string[] args)
        {
            int port = 7000;
            if (args.Length == 0)
            {
                Console.WriteLine("No port was passed as argument, default port: 7000");
                Start(port);
            }
            else if (args.Length > 1)
            {
                Console.WriteLine("Invalid Argument, .exe [port]");
            }
            else
            {
                try
                {
                    port = Convert.ToInt32(args[0]);
                    Start(port);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Argument, .exe [port]");
                }
            }



        }
        private static void Start(int port)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(1);
            Dictionary<string, ConnectionModel> ClientList = new Dictionary<string, ConnectionModel>();
            Console.WriteLine("<SERVER ON LISTENING ON PORT {0}>", port);

            while (true)
            {
                ConnectionModel connection = new ConnectionModel();
                connection.socket = serverSocket.Accept();
                new HandleConnection(connection, ClientList);
            }
        }
    }
}

