using System;
using System.Collections.Generic;

namespace HW3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read the text as a string value and calculate the counts of characters 'a', 'o', 'i', 'e' in this text.

            Console.WriteLine("Please write some text");
            string userString = Console.ReadLine();

            Console.WriteLine("Write chars as a string the count of which you want to test");
            char[] charsToTest = Console.ReadLine().ToCharArray();

            foreach (char k in charsToTest)
            {
                int occurenceCnt = 0;
                foreach (char i in userString)
                    {
                        if (i == k) occurenceCnt++;
                    }

                Console.WriteLine("Char \"" + k + "\" is found " + occurenceCnt + " times");
            }


            //Ask user to enter the number of month. Read the value and write the amount of days in this month.

            Console.WriteLine("Please enter an integer that represents a number of month");
            string userMonth = Console.ReadLine();
            int monthInt;

            if (int.TryParse(userMonth, out monthInt))
            {
                Console.WriteLine("Number processed");
                if (monthInt>=1 && monthInt<=12)
                {
                    int daysCnt = DateTime.DaysInMonth(DateTime.Now.Year, monthInt);
                    Console.WriteLine("There are " + daysCnt + " days in " + monthInt + " month");
                }
                else
                {
                    Console.WriteLine("Number does not represent a month");
                }
            }
            else
            {
                Console.WriteLine("Please enter a number");
            }


            //Enter 10 integer numbers. Calculate the sum of first 5 elements if they are positive or product of last 5 element in  the other case.

            Console.WriteLine("You will be prompted to enter 10 integer values, please do so");

            int sumOfFirst = 0;
            int prodOfLast = 0;
            int hasNeg = 0;

            for (int a = 1; a < 11; a++)
            {
                Console.WriteLine("Input number " + a);
                string numToTest = Console.ReadLine();
                int intToTest;

                if (int.TryParse(numToTest, out intToTest))
                {
                    Console.WriteLine("Number processed");
                    if (intToTest<0)
                    {
                        hasNeg++;
                    }

                    if (a<=5)
                    {
                        sumOfFirst += intToTest;
                    }
                    else if (a ==6)
                    {
                        prodOfLast += intToTest;
                    }
                    else
                    {
                        prodOfLast *= intToTest;
                    }
                    
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            }

            if (hasNeg != 0)
            {
                Console.WriteLine("Product of the last five items is " + prodOfLast);
            }
            else
            {
                Console.WriteLine("Sum of first five elements is " + sumOfFirst);
            }
        }
    }
}
