using System.Net;
using System.Net.Sockets;
using System.Text;

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
        var clients = new List<Socket>();

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
            var client = listener.Accept();
            clients.Add(client);
            Console.WriteLine($"{listener.RemoteEndPoint} connected...");


            Task.Run(() =>
            {
                var buffer = new byte[1024];
                var len = 0;
                var msj = string.Empty;

                while (true)
                {
                    len = client.Receive(buffer);
                    msj = Encoding.Default.GetString(buffer, 0, len);

                    Console.WriteLine($"{listener.RemoteEndPoint}: {msj}");


                    if(msj == "exit")
                    {
                        client.Shutdown(SocketShutdown.Both);
                    }

                    foreach (var c in clients)
                    {
                        if (client != c)
                            c.Send(Encoding.Default.GetBytes(msj));
                    }
                }


                
            });


            
        }






    }
}