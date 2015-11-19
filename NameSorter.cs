using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameSorter
{
    public class NameSorter
    {
        private string[] args;
        
        public NameSorter(string[] args)
        {
            this.args = args;
        }

        public void Run()
        {
            try
            {
                if (args.Length != 1)
                {
                    Console.WriteLine("Invalid argument. Argument should be the path of the file to sort");
                    return;
                }

                var inputFilePath = args[0];

                //Get the input file
                var lines = File.ReadAllLines(inputFilePath);
                Console.WriteLine("sort-names {0}", inputFilePath);

                //Convert to Person objects, sort and convert back to lines
                var people = lines.Select(Person.CreateFromLine).ToList();
                people.Sort();
                var sortedLines = Person.GetFormattedLinesFromPeople(people);

                //Print them all back to console
                foreach (var line in sortedLines)
                {
                    Console.WriteLine(line);
                }

                var outputFilePath = AppendToFileName(inputFilePath, "-sorted");

                File.WriteAllLines(outputFilePath, sortedLines);
                Console.WriteLine("Finished: created {0}", outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal static string AppendToFileName(string filePath, string toAppend)
        {
            return Path.Combine(Path.GetDirectoryName(filePath),
                String.Format("{0}{1}{2}",
                    Path.GetFileNameWithoutExtension(filePath),
                    toAppend,
                    Path.GetExtension(filePath)));
        }
    }
}