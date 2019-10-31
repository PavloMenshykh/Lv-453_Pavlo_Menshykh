using System;

namespace classwork
{
    class Program

    { 
        enum TestCaseStatus
        {
        Pass,
        Fail, 
        Blocked,
        WP, 
        Unexecuted
        }
    
        static void Main(string[] args)
        {
            int day, month;
            Console.WriteLine("Please enter an integer" +
                " that represents a day");
            day = System.Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter an integer" +
                " that represents a month");
            month = System.Convert.ToInt32(Console.ReadLine());

            string result = ((day < 31) && (month <= 12)) ?
                "possible" : "not possible";

            Console.WriteLine("Input values for day and month are " +
                result);

            string numberToTest;
            Console.WriteLine("Please enter a float number");
            numberToTest = Console.ReadLine();

            String[] separator = { "," };
            String[] parts = numberToTest.Split(separator, 2,
                StringSplitOptions.RemoveEmptyEntries);

            int numOne, numTwo;

            numOne = (int)parts[1][0];
            numTwo = (int)parts[1][1];

            Console.WriteLine("{0},{1}", numOne, numTwo);

            int resultFloat = numOne + numTwo;

            Console.WriteLine("The result of the first two values after comma is " + resultFloat);

            Console.WriteLine("Please enter time in 24 hour format as integer");
            int hourInput = System.Convert.ToInt32(Console.ReadLine());
            string greetings = ((0 <= hourInput) && (hourInput<6)) ? 
                "Good night" : 
                ((6<=hourInput) && (hourInput<12)) ?
                "Good morning":
                ((12<=hourInput) && (hourInput<18)) ?
                "Good afternoon":
                "Good evening";

            Console.WriteLine(greetings);

            //Enum task
            TestCaseStatus test1Status = TestCaseStatus.Pass;
            Console.WriteLine(test1Status);
        }
    }
}
