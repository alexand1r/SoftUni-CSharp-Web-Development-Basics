namespace _2._ValidateUrl
{
    using System;
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {
            var url = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(url);
            var port = 0;

            var uri = new Uri(decodedUrl);

            if (string.IsNullOrEmpty(uri.Scheme))
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            if (string.IsNullOrEmpty(uri.Host) || !uri.Host.Contains("."))
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            if (string.IsNullOrEmpty(uri.Port.ToString()))
            {
                if (uri.Scheme == "http")
                    port = 80;
                else port = 443;
            }
            else if ((uri.Scheme == "http" && uri.Port == 443)
                     || (uri.Scheme == "https" && uri.Port == 80))
            {
                Console.WriteLine("Invalid URL");
                return;
            }
            else port = uri.Port;

            if (string.IsNullOrEmpty(uri.AbsolutePath) || uri.AbsolutePath[0] != '/')
            {
                Console.WriteLine("Invalid URL");
                return;
            }

            Console.WriteLine($"Protocol: {uri.Scheme}");
            Console.WriteLine($"Host: {uri.Host}");
            Console.WriteLine($"Port: {port}");
            Console.WriteLine($"Path: {uri.AbsolutePath}");

            if (!string.IsNullOrEmpty(uri.Query))
            {
                Console.WriteLine($"Query: {uri.Query}");
            }
            if (!string.IsNullOrEmpty(uri.Fragment))
            {
                Console.WriteLine($"Fragment: {uri.Fragment}");
            }
        }
    }
}
