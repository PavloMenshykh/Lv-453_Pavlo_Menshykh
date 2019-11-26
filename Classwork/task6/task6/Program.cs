using System;
using System.Collections.Generic;

namespace task6
{
    class Program
    {
        static void Main(string[] args)
        {
            int numone;
            int numtwo;

            int rslt = 0;

        tryNum:
            Console.WriteLine("Enter two int numbers");
            numone = Convert.ToInt32(Console.ReadLine());
            numtwo = Convert.ToInt32(Console.ReadLine());
            
            try
            {
                rslt = div(numone, numtwo);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Try again");
                goto tryNum;
            }

            Console.WriteLine("Succes, result is "+rslt);
            Console.ReadKey();

            List<int> toFill = new List<int>();

            Console.WriteLine("Enter start and end test numbers");
            int start = Convert.ToInt32(Console.ReadLine());
            int end = Convert.ToInt32(Console.ReadLine());

            int lastnum = start+1;

            Console.WriteLine("start entering numbers");

            while (lastnum<end)
            {
                int testnum = ReadNumber(lastnum);
                if (testnum > lastnum)
                {
                    lastnum = testnum;
                    toFill.Add(lastnum);
                }
                else
                {
                    Console.WriteLine("number is smaller");
                }
            }

            Console.WriteLine("list of nums");
            foreach (int i in toFill)
            {
                Console.WriteLine(i);
            }
        }


        public static int div(int one, int two)
        {
            int result = one / two;
            return result;
        }

        public static int ReadNumber(int lastnum)
        {
            int numToTest = 0;
        readHere:
            try
            {
                numToTest = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception) 
            {
                Console.WriteLine("Value is not a number");
                goto readHere;
            }
            return numToTest;
        }
    }
}
