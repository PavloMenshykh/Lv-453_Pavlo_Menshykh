using System;
using System.Collections.Generic;
using System.Linq;

namespace task9
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a collection of 10 integers numders
            //Get and display only negative numbers on the console
            //Get and display only positive numbers on the console
            //Get the largest and smallest elements from the array, and find the sum of all elements of the array.
            //Get the first largest element in array that is smaller than the Average of elements in array
            //Sort the array using OrderBy

            int countnums = 10;
            var rand = new Random();
            List<int> rndlist = new List<int>();

            for (int i = 0; i < countnums; i++)
            {
                rndlist.Add(rand.Next(-50, 50));
            }

            Console.WriteLine("Numbers generated:");
            foreach(int i in rndlist)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("===========");

            Console.WriteLine("Negative numbers:");
            var negatives = from i in rndlist
                            where i < 0
                            select i;
            foreach (var i in negatives)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("===========");

            Console.WriteLine("Positive numbers:");
            var positives = from i in rndlist
                            where i > 0
                            select i;
            foreach (var i in positives)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("===========");

            int min = rndlist.Min();
            Console.WriteLine("Smallest element:"+min);

            int max = rndlist.Max();
            Console.WriteLine("Largest element:" + max);

            int sum = rndlist.Sum();
            Console.WriteLine("Sum of elements:" + sum);

            var underAvg = rndlist.Where(i => i < sum);
            var largestUnderAvg = underAvg.Max();
            Console.WriteLine("Largest under average: " + largestUnderAvg);
            Console.WriteLine("===========");

            var orderedBy = rndlist.OrderBy(i => i);

            Console.WriteLine("Numbers sorted:");
            foreach (int i in orderedBy)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("===========");
        }
    }
}
