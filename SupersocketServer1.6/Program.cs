using System;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;

namespace SupersocketServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start the server!");

            var bootstrap = BootstrapFactory.CreateBootstrap();

            if (!bootstrap.Initialize())
            {
                Console.WriteLine("Failed to initialize!");
                Console.ReadKey();
                return;
            }

            var result = bootstrap.Start();

            Console.WriteLine("Start result: {0}!", result);

            if (result == StartResult.Failed)
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}