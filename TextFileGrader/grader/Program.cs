using System;
using System.IO;
using System.Linq;
using System.Text;

// Follows SRP: One purpose per class or method
namespace grader
{
    class Program
    {
        // driver method to facilitate unit testing
        static void RunUnitTest(string path)
        {
            if (graderTests.UnitTestgrader.UnitTest1(path))
            {
                System.Console.WriteLine("Passes unit test#1");
            }
            else
            {
                System.Console.WriteLine("Fails unit test#1");
            }
        }

        // text file sorter logic
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                System.Console.WriteLine("Please provide input txt file name.");
                return;
            }


            string filePath = args[0];

            string[] lines = File.ReadAllLines(filePath, Encoding.Default);

            var sorted = lines.Select(line => new
            {
                firstName = line.Split(',')[0],
                lastName = line.Split(',')[1],
                score = Int32.Parse(line.Split(',')[2]),
                thisLine = line
            }
            ).OrderByDescending(x => x.score).ThenBy(x => x.firstName).ThenBy(x => x.lastName). Select(x => x.thisLine);

            string targetFile = Path.GetFileNameWithoutExtension(filePath) + "-graded.txt";
            File.WriteAllLines(targetFile, sorted.ToArray(), Encoding.Default);

            System.Console.WriteLine("Output file: {0}", targetFile);

            RunUnitTest(targetFile);
        }
    }
}
