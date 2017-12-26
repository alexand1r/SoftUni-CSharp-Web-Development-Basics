namespace _3.SimpleWebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices.ComTypes;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");
            int port = 1300;
            TcpListener listener = new TcpListener(address, port);
            listener.Start();

            Console.WriteLine($"Server started.");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1:{port}");

            var task = Task.Run(() => ConnectWithTcpClient(listener));
            task.Wait();
        }

        private static async void ConnectWithTcpClient(TcpListener listener)
        {
            while (true)
            {
                Console.WriteLine("Waiting for client...");
                var client = await listener.AcceptTcpClientAsync();

                Console.WriteLine("Client connected.");

                // Read the request and print it on the console
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, buffer.Length);

                var message = Encoding.ASCII.GetString(buffer);
                Console.WriteLine(message);

                // Send a greeting to the client
                byte[] data = Encoding.ASCII.GetBytes("Hello from server!");
                client.GetStream().Write(data, 0, data.Length);

                // Close the connection
                Console.WriteLine("Closing the connection");
                client.GetStream().Dispose();
            }
        }
    }
}
