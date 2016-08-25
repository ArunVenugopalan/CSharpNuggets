using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace grader.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        // Helper method to provide some test inputs       
        List<Tuple<string, string>> GetInputs()
        {
            return new List<Tuple<string, string>>
            {   //random contents
                Tuple.Create( "Tests\\input1.txt", "Tests\\sorted-input1.txt" ),
                //same contents 
                Tuple.Create( "Tests\\input2.txt", "Tests\\sorted-input2.txt" ),
                // Already sorted
                Tuple.Create( "Tests\\input3.txt", "Tests\\sorted-input3.txt" ),
                // Empty file
                Tuple.Create( "Tests\\input4.txt", "Tests\\sorted-input4.txt" ),
                // ascending order (reverse sorted)
                Tuple.Create( "Tests\\input5.txt", "Tests\\sorted-input5.txt" )
            };
        }

        // Unit test FileIO::GetContents
        [TestMethod()]
        public void GetContentsTest()
        {
            foreach (var tuple in GetInputs())
            {
               string filePath = tuple.Item1;
               string[] expectedContents = File.ReadAllLines(filePath, Encoding.Default);
               string[] actualContents = FileIO.GetContents(filePath);

               Assert.IsTrue(expectedContents.SequenceEqual(actualContents),
                   "GetContents: content mismatch test " + tuple + " failed");
            }
        }

        // Unit test FileIO::WriteContents
        [TestMethod()]
        public void WriteFileContentsTest()
        {
            string[] expectedContents = 
            {
                "BUNDY, TERESSA, 88",
                "KING, MADISON, 88",
                "SMITH, FRANCIS, 85",
                "SMITH, ALLAN, 70"
            };

            string filePath = "sample.txt";
            FileIO.WriteContents(filePath, expectedContents);
            string[] actualContents = File.ReadAllLines(filePath, Encoding.Default);

            Assert.IsTrue(expectedContents.SequenceEqual(actualContents), 
                "WriteContents: content mismatch test failed");
        }

        // Unit test Sorter::SortContents
        [TestMethod()]
        public void SortFileTest()
        {
            foreach (var tuple in GetInputs())
            {
                string[] expectedContents = File.ReadAllLines(tuple.Item2, Encoding.Default);
                string[] actualContents = Sorter.SortContents(File.ReadAllLines(tuple.Item1, Encoding.Default));

                Assert.IsTrue(expectedContents.SequenceEqual(actualContents),
                    "SortContents: Content is not sorted as per requirements " + tuple);
            }
        }

        // Unit test Sorter::GetSortedFile
        [TestMethod()]
        public void GetSortedFileTest()
        {
            foreach (var tuple in GetInputs())
            {
                string filePath = tuple.Item1;
                string sortedfileForTest = tuple.Item2;
                string expectedFileName = Path.GetFileNameWithoutExtension(tuple.Item1) + "-graded.txt";

                string[] expectedContents = File.ReadAllLines(sortedfileForTest, Encoding.Default);
                string actualfileName = Sorter.GetSortedFile(filePath);
                string[] actualContents = File.ReadAllLines(actualfileName, Encoding.Default);

                Assert.IsTrue(expectedFileName.Equals(actualfileName),
                    "GetSortedFile: Output file name isn't named as per requirements");
                Assert.IsTrue(expectedContents.SequenceEqual(actualContents),
                    "GetSortedFile: Sorting test failed");
            }
        }
    }
}