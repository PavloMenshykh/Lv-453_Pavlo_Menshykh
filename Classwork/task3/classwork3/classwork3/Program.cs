using System;
using System.Collections.Generic;
using System.Linq;

namespace classwork3
{


    class Program
    {
        static void Main(string[] args)
        {
            //Enter a and b are two integers. Calculate how many 
            //integers in the range [a..b] are divided by 3 without 
            //remainder.

            Console.WriteLine("Please enter two integer numbers");
            int intOne = System.Convert.ToInt32(Console.ReadLine());
            int intTwo = System.Convert.ToInt32(Console.ReadLine());

            int divThreeCnt = 0;

            int intLrgr;
            int intSmlr;

            if (intOne > intTwo)
            {
                intLrgr = intOne;
                intSmlr = intTwo;
            }
            else
            {
                intLrgr = intTwo;
                intSmlr = intOne;
            }

            for (int i = intSmlr; i<intLrgr; i++)
            {
                if (i % 3 == 0)
                divThreeCnt += 1;
            }

            Console.WriteLine("There are " + divThreeCnt + " numbers divisible by three");


            //Enter a character string. Print each second character

            Console.WriteLine("Enter some chars");
            string writeSecond = Console.ReadLine();

            for (int i = 0; i < writeSecond.Length; i += 2)
            {
                Console.WriteLine(writeSecond[i]);
            }


            //Enter the name of the drink (coffee, tea, juice, water). 
            //Print the name of the drink and its price.

            Console.WriteLine("Enter the name of one of the drinks: " +
                "coffee, tea, juice, water");

            string userDrink = Console.ReadLine();

            int price = (userDrink == "coffee") ? 45 :
            (userDrink == "tea") ? 25 :
            (userDrink == "juice") ? 35 :
            (userDrink == "water") ? 10 :
            0;

            if (price == 0)
            {
                Console.WriteLine("this is not on the menu");
            }
            else
            {
                Console.WriteLine(userDrink + " costs " + price);
            }


            //Enter a sequence of positive integers (to the first negative). 
            //Calculate the arithmetic average of the entered numbers.

            List<int> numberHolder = new List<int>();

            Console.WriteLine("Please enter numbers you want to know the average of" +
                "stop calculation by entering a negative number");

            int lastNum = 1;

            while (lastNum>0)
            {
                string inputInt = Console.ReadLine();
                int aInt;
                if (int.TryParse(inputInt, out aInt))
                {
                    if (aInt > 0)
                    {
                        numberHolder.Add(aInt);
                    }
                    lastNum = aInt;
                    Console.WriteLine("Number processed");
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            }

            double avgNum = numberHolder.Average();

            Console.WriteLine("The avarage of positive numbers is " + avgNum);


            //Check whether the entered year is a leap.

            Console.WriteLine("Please enter the year");
            int year = System.Convert.ToInt32(Console.ReadLine());

            if (year%4 == 0 && year%100 != 0)
            {
                Console.WriteLine("Vysokosnuy");
            }
            else if (year%4 == 0 && year % 100 == 0 && year % 400 == 0)
            {
                Console.WriteLine("Vysokosnuy");
            }
            else
            {
                Console.WriteLine("Ne vysokosnuy");
            }


            //Find the sum of digits of the entered integer number

            Console.WriteLine("Please enter an integer");
            string numStr = Console.ReadLine();

            int sumNums = 0;

            foreach (char i in numStr)
            {
                sumNums += (int)char.GetNumericValue(i);
            }

            Console.WriteLine("The sum of numbers is " + sumNums);


            //Check whether the entered integer number contains only odd number
            Console.WriteLine("Please enter an integer");
            string numStr2 = Console.ReadLine();

            int checker2 = 0;


            foreach (char i in numStr)
            {
                int internalCheck =  (int)char.GetNumericValue(i);
                if (internalCheck%2 != 0)
                {
                    checker2 += 1;
                }
            }

            if (checker2 == numStr.Count())
            {
                Console.WriteLine("only odd");
            }
            else
            {
                Console.WriteLine("Not only odd");
            }
        }
    }
}
