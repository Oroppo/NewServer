using System;
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
            Console.WriteLine("Network Has Been Initialized");
        }
        private static void ConsoleThread()
        {
            while (true)
            {

            }
        }
    }
}
