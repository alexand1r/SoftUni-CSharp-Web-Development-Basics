namespace _2.SliceFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    class Program
    {
        private static List<string> files = new List<string>();
        private static MatchCollection matches;

        static void Main(string[] args)
        {
            string videoPath = Console.ReadLine();
            string destination = Console.ReadLine();
            int pieces = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destination, pieces);

            Console.WriteLine("Anything else?");
            while (true) Console.ReadLine();
        }

        private static void SliceAsync(string videoPath, string destination, int pieces)
        {
            Task.Run(() =>
            {
                Slice(videoPath, destination, pieces);
            });

        }

        private static void Slice(string sourceFile, string destinationPath, int parts)
        {
            using (var source = new FileStream(sourceFile, FileMode.Open))
            {
                long partSize = (long)Math.Ceiling((double)source.Length / parts);

                // The offset at which to start reading from the source file
                long fileOffset = 0;
                ;
                string currPartPath;
                FileStream fsPart;
                long sizeRemaining = source.Length;

                // extracting name and extension of the input file
                string pattern = @"(\w+)(?=\.)\.(?<=\.)(\w+)";
                Regex pairs = new Regex(pattern);
                matches = pairs.Matches(sourceFile);
                for (int i = 0; i < parts; i++)
                {
                    currPartPath = destinationPath + matches[0].Groups[1] + String.Format(@"-{0}", i) + "." + matches[0].Groups[2];
                    files.Add(currPartPath);

                    // reading one part size
                    using (fsPart = new FileStream(currPartPath, FileMode.Create))
                    {
                        byte[] buffer = new byte[partSize];

                        int readBytes = source.Read(buffer, 0, buffer.Length);

                        // creating one part size file
                        fsPart.Write(buffer, 0, readBytes);
                    }

                    // calculating the remaining file size which iis still too be read
                    sizeRemaining = (int)source.Length - (i * partSize);
                    if (sizeRemaining < partSize)
                    {
                        partSize = sizeRemaining;
                    }
                    fileOffset += partSize;
                }
            }
        }
    }
}
