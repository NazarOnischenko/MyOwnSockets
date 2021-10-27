using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyOwnSockets
{
    class Program
    {
        static void Main(string[] args)
        {
            const string id = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(id), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(4);
            while (true)
            {
                var listener = tcpSocket.Accept();
                var buffer = new byte[256];
                int size;
                var data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));

                } while (tcpSocket.Available > 0);

                Console.WriteLine(data.ToString());
                using (var sw = new StreamWriter("products.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine(data.ToString());
                }

                listener.Send(Encoding.UTF8.GetBytes("Success"));
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();
        }
    }
}
