using System;
using System.IO;
using System.Linq;
using System.Text;

// Follows SRP: One purpose per class or method

namespace grader
{

    // Purpose of this class is to facilitate file IO operations

    // [ Future improvements: class FileIO can be derived from an abstract GenericIO class for future extensibility
    // The purpose of the GenericIO class is to provide consistent interface irrespective of
    // the underlying physical/logical infrastructure such as FS, Databases, DFS, Networked IO devices, so on]
    public class FileIO
    {
        // API to fetch file contents
        public static string[] GetContents(string filePath)
        {
            return File.ReadAllLines(filePath, Encoding.Default);
        }

        // API to write file contents 
        public static void WriteContents(string filePath, string[] fileContents)
        {
            File.WriteAllLines(filePath, fileContents, Encoding.Default);
        }
    }

    // Purpose of this class is to facilitate sorting
    // At the moment, this class sort contents by <grade>, <last-name> & <first-name> respectively.

    //[ Future improvements: The order of sorting can be refactored to implement sorting-policy that supports
    // different sorting orders. The sorting-policy can be implemented using enum and/or by implementing 
    // dedicated sorting classes using strategy pattern].
    public class Sorter
    {
        // API to sort contents
        public static string[] SortContents(string[] contents)
        {
            var sorted = contents.Select(line => new
            {
                lastName = line.Split(',')[0],
                firstName = line.Split(',')[1],
                score = Int32.Parse(line.Split(',')[2]),
                thisLine = line
            }
            ).OrderByDescending(x => x.score).ThenBy(x => x.lastName).ThenBy(x => x.firstName).Select(x => x.thisLine);

            return sorted.ToArray();
        }

        // API to sort input file. By default, it sorts by grade, last-name & first name
        // The sorting behvaiour can be extended by using enum to specify sort type as second parameter
        public static string GetSortedFile(string inputFile)
        {
            // Get source and target file paths
            string sourcePath = inputFile;
            string targetFile = Path.GetFileNameWithoutExtension(sourcePath) + "-graded.txt";

            string[] fileContents = FileIO.GetContents(sourcePath);
            string[] fileContentsSorted = SortContents(fileContents);
            FileIO.WriteContents(targetFile, fileContentsSorted);

            return targetFile;
        }

        // text file sorter logic
        static void Main(string[] args)
        {
            // Validate inputs before going further
            if (args.Length == 0)
            {
                // Can't process further! Inform user and exit
                System.Console.WriteLine("Please provide input txt file name.");
                return;
            }

            System.Console.WriteLine("Output file: {0}", GetSortedFile(args[0]));
        }
    }
}
