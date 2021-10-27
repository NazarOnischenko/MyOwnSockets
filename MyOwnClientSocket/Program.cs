using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyOwnClientSocket
{
    class Program
    {
        static void Main(string[] args)
        {

            const string id = "127.0.0.1";
            const int port = 8080;
            bool end = true;
        
            while (end)
            {
                Console.WriteLine("Wrire name of product:");
                var name = Console.ReadLine();
                Console.WriteLine("Write calories of product:");
                int calories = int.Parse(Console.ReadLine());
                Console.WriteLine("Write weight of product:");
                int weight = int.Parse(Console.ReadLine());
                Product product = new Product(name, calories, weight);
                var tcpEndPoint = new IPEndPoint(IPAddress.Parse(id), port);

                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                var buffer = new byte[256];
                int size;

                var message = $"{product.Name}, {product.Calories} kkal, {product.Weight} gr";
                var data = Encoding.UTF8.GetBytes(message);


                tcpSocket.Connect(tcpEndPoint);
                tcpSocket.Send(data);
                Console.WriteLine("Do you want to finish work?(y)");
                if (Console.ReadLine() == "y")
                {
                    end = false;
                }
                var answer = new StringBuilder();
                do
                {
                    size = tcpSocket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                    tcpSocket.Send(data);

                } while (tcpSocket.Available > 0);
                Console.WriteLine(answer.ToString());
               
                tcpSocket.Shutdown(SocketShutdown.Both);
                tcpSocket.Close();
            }
        }
    }
}
