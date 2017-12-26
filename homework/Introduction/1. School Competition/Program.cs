namespace _1.School_Competition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        private const string EndMessage = "END";
        private static Dictionary<string, Dictionary<string, int>> students;
        static void Main(string[] args)
        {
            students = new Dictionary<string, Dictionary<string, int>>();
            var line = Console.ReadLine();
            while (!line.Equals(EndMessage))
            {
                var data = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var name = data[0];
                var subject = data[1];
                var points = int.Parse(data[2]);

                AddDataToDict(name, subject, points);

                line = Console.ReadLine();
            }

            PrintDict();
        }

        private static void PrintDict()
        {
            var sb = new StringBuilder();
            foreach (var kvp in students
                .OrderByDescending(s => s.Value.Sum(v => v.Value))
                .ThenBy(s => s.Key))
            {
                sb.AppendLine($"{kvp.Key}: {kvp.Value.Sum(v => v.Value)} [{string.Join(", ", kvp.Value.Keys.OrderBy(s => s))}]");
            }
            Console.WriteLine(sb.ToString());
        }

        private static void AddDataToDict(string name, string subject, int points)
        {
            if (!students.ContainsKey(name))
            {
                students.Add(name, new Dictionary<string, int>());
            }
            if (!students[name].ContainsKey(subject))
            {
                students[name].Add(subject, 0);
            }
            students[name][subject] += points;
        }
    }
}