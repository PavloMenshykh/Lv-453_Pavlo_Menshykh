using System;
using System.Collections.Generic;
using System.Linq;

namespace HW2
{
    enum httpErrors
    {
        Bad_Request = 400, 
        Unauthorized = 401,
        Payment_Required = 402,
        Forbidden = 403,
        Not_Found = 404,
        Method_Not_Allowed = 405,
        Not_Acceptable = 406
    }

    public struct Dog
    {
        public string Name;
        public string Breed;
        public string Age;

        public override string ToString()
        {
            return string.Format("This is {0} he is a {1} and he's {2} years old", Name, Breed, Age);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //read 3 float numbers and check: are they all belong to the range [-5,5].

            Console.WriteLine("Please enter 3 floats to check for -5 to 5 domain inclusion");

            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine("Please enter number {0}", i);
                string inputA = Console.ReadLine();
                float a;
                if (float.TryParse(inputA, out a))
                {
                    string checkResult = ((-5 <= a) && (a <= 5)) ?
                    "falls within -5 to 5 domain" :
                    "does not fall within -5 to 5 domain";
                    Console.WriteLine(a + " " + checkResult);
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            }


            //read 3 integers and write max and min of them.

            List<int> numberStHolder = new List<int>();

            Console.WriteLine("Please enter 3 integers to find the in and max value");

            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine("Please enter number {0}", i);
                string inputInt = Console.ReadLine();
                int aInt;
                if (int.TryParse(inputInt, out aInt))
                {
                    numberStHolder.Add(aInt);
                    Console.WriteLine("Number processed");
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            }

            int resultMax = numberStHolder.Max();
            int resultMin = numberStHolder.Min();

            Console.WriteLine("The largest integer from entered above is " + resultMax);
            Console.WriteLine("The smallest integer from entered above is " + resultMin);


            //read number of HTTP Error (400, 401,402, ...) and write the name of this error (Declare enum HTTPError)

            Console.WriteLine("Please enter the number of http error, only errors 400 to 406 are supported");
            string httpStr = Console.ReadLine();

            int httpInt;
            if (int.TryParse(httpStr, out httpInt))
            {
                if ((400 <= httpInt) && (httpInt <= 406))
                {
                    httpErrors myError = (httpErrors)httpInt;
                    Console.WriteLine(myError);
                }
                else
                {
                    Console.WriteLine("Error is outside of processable range");
                }
            }
            else
            {
                Console.WriteLine("Please enter a number");
            }


            //declare struct Dog with fields Name, Mark, Age. Declare variable myDog of Dog type and read values for it. 
            //Output myDog into console. (Declare method ToString in struct)

            Dog MyDog;
            Console.WriteLine("Please enter your dogs name");
            MyDog.Name = Console.ReadLine();
            Console.WriteLine("Please enter the breed of your dog");
            MyDog.Breed = Console.ReadLine();
            Console.WriteLine("Please enter your dogs age");
            MyDog.Age = Console.ReadLine();
            Console.WriteLine(MyDog);
        }
    }
}
