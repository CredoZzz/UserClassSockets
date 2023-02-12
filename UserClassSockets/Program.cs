using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8080);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Bind(endPoint);
                socket.Listen(10);

                Console.WriteLine("Server started, waiting for connections...");
                Socket client = socket.Accept();
                Console.WriteLine("Client connected");

                byte[] buffer = new byte[1024];
                int bytesReceived = client.Receive(buffer);
                string name = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                Console.WriteLine("Received name: " + name);
                string message = "Hello " + name;
                byte[] data = Encoding.UTF8.GetBytes(message);

                client.Send(data);
                Console.WriteLine("Sent message: " + message);
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                socket.Close();
            }
        }
    }
}