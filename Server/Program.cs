using System.Net;
using System.Net.Sockets;

namespace Server;

/*

Server Qurmaq ucun mene neler lazimdir:

1. OS           -> operation system
2. Web Service  -> Apache, IIS ( Windows ), Nginx
3. Traslator    -> C#, Php, Java
 
*/


class Program
{
    static void Main()
    {
        var listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        var ip = IPAddress.Parse("127.0.0.1");

        var port = 8005;

        // EndPoint -> ip + port -> 127.0.0.1:8005

        var ep = new IPEndPoint(ip, port);

        listener.Bind(ep);

        // |
        var backlog = 1;

        listener.Listen(backlog);

        Console.WriteLine($"Server listen on {listener.LocalEndPoint}");

        while (true)
        {
            listener.Accept();

            Console.WriteLine($"{listener.RemoteEndPoint} connected...");
        }






    }
}