using System;
using System.Net;
using System.Threading;

namespace NewServer
{
    class Program
    {
        private static Thread threadConsole;
        private static string hostName = Dns.GetHostName();
        static void Main(string[] args)
        {
            threadConsole = new Thread(new ThreadStart(ConsoleThread));
            threadConsole.Start();

            NetworkConfig.InitNetwork();
            NetworkConfig.socket.StartListening(8888, 5, 1);
            Console.WriteLine("Network Has Been Initialized " + Dns.GetHostEntry(hostName).AddressList[1].ToString());
        }
        private static void ConsoleThread()
        {
            while (true)
            {

            }
        }
    }
}
