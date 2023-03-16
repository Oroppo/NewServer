using System;
using System.Net;
using System.Threading;

namespace NewServer
{
    class Program
    {
        private static Thread threadConsole;
        static void Main(string[] args)
        {
            threadConsole = new Thread(new ThreadStart(ConsoleThread));
            threadConsole.Start();

            NetworkConfig.InitNetwork();
            NetworkConfig.socket.StartListening(8888, 5, 1);
            Console.WriteLine("Network Has Been Initialized at the IP: {0} ", Dns.GetHostEntry(Dns.GetHostName()).AddressList[1]);
        }
        private static void ConsoleThread()
        {
            while (true)
            {

            }
        }
    }
}
