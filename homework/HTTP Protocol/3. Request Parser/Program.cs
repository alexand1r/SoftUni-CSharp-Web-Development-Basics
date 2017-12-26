namespace _3._RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {
            var paths = new Dictionary<string, HashSet<string>>();

            var cmd = Console.ReadLine();
            while (!cmd.Equals("END"))
            {
                string[] data = cmd.Split(new [] {'/'}, StringSplitOptions.RemoveEmptyEntries);
                string actionn = data[0];
                string methodd = data[1];

                if (!paths.ContainsKey(actionn))
                    paths.Add(actionn, new HashSet<string>());

                paths[actionn].Add(methodd);

                cmd = Console.ReadLine();
            }

            string[] request = Console.ReadLine().Split();
            string method = request[0];
            string url = request[1];
            var action = url.Substring(1, url.Length-1).ToLower();
            bool isValid = paths.ContainsKey(action) && paths[action].Contains(method.ToLower());
            int statusCode = isValid ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound;

            var statusText = statusCode == (int) HttpStatusCode.OK ? "OK" : "NotFound";
            var contentLength = statusText.Length;

            Console.WriteLine($"{request[2]} {statusCode} {statusText}");
            Console.WriteLine($"Content-Length: {contentLength}");
            Console.WriteLine($"Content-Type: text/plain");
            Console.WriteLine();
            Console.WriteLine($"{statusText}");
        }
    }
}
