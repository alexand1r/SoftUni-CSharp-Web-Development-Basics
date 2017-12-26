namespace DecodeUrl
{
    using System;
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {
            var url = Console.ReadLine();

            Console.WriteLine(WebUtility.UrlDecode(url));
        }
    }
}
