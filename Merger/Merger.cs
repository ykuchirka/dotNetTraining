using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Merget
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length<3) 
            {
                System.Console.WriteLine("Params are not enough");
                System.Console.ReadKey();
                return;
            }

            string inputFile1 = args[0];
            string inputFile2 = args[1];
            string outputFile = args[2];

            List<int> inputListInt1 = new List<int> {};
            List<int> inputListInt2 = new List<int> {};
            
            ReadIntList(inputFile1, inputListInt1);
            ReadIntList(inputFile2, inputListInt2);

            inputListInt1.AddRange(inputListInt2);
            inputListInt1.Sort();

            WriteIntList(outputFile, inputListInt1);

            System.Console.ReadKey();     
        }

        private static void WriteIntList(string fileName, List<int> list)
        {
            try
            {
                using (var infile = new StreamWriter(fileName))
                {
                    foreach (var curInt in list)
                    {
                        System.Console.WriteLine(curInt);
                        infile.WriteLine(curInt);
                    }
                }
            }
            catch (Exception e) 
            {
                System.Console.WriteLine("Error writeing file " + e.Message);
            }
        }

        private static void ReadIntList(string fileName, List<int> list)
        {
            string line;
            int curInt;
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        try
                        {
                            curInt = Int32.Parse(line);
                            list.Add(curInt);
                        }
                        catch (Exception e)
                        { }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error reading file" + e.Message);          
            }
        }
    }
}
