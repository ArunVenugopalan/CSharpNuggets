using System;

namespace graderTests
{
    // class to encapsulate all unit test cases
    class UnitTestgrader
    {
        // Sample unit test
        public static Boolean UnitTest1(string path)
        {
            Boolean result = true;

            string[] sorted_sample =
            {
                "BUNDY, TERESSA, 88",
                "KING, MADISON, 88",
                "SMITH, FRANCIS, 85",
                "SMITH, ALLAN, 70"
            };

            string line;
            int index = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                if (line != sorted_sample[index])
                {
                    result = false;
                    break;
                }
                ++index;
            }
                
            return result;
        }
    }

    // Add more unit tests here
}
