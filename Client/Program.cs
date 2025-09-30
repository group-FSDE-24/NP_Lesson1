using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client;

class Program
{
    static void Main(string[] args)
    {
        var client = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp);

        var ip = IPAddress.Parse("127.0.0.1");

        var port = 8005;

        // EndPoint -> ip + port -> 127.0.0.1:8005

        var serverEndPoint = new IPEndPoint(ip, port);

        try
        {
            client.Connect(serverEndPoint);

            if (client.Connected)
            {
                Console.WriteLine("Connected to Server...");

                Task.Run(() =>
                {
                    var buffer = new byte[1024];
                    var len = 0;
                    var msj = string.Empty;

                    while (true)
                    {
                        len = client.Receive(buffer);
                        msj = Encoding.Default.GetString(buffer, 0, len);

                        Console.WriteLine(msj);
                    }

                });

                while (true)
                {
                    var msj = Console.ReadLine(); // Ayan oyan
                    var buffer = Encoding.Default.GetBytes(msj);

                    client.Send(buffer);
                }

            }
            else
            {
                Console.WriteLine("Server can not be connected....");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Server can not be connected....");
            Console.WriteLine($"Ex: {ex.Message}");
        }
    }
}