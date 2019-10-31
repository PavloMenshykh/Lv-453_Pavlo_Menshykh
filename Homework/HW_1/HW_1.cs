using System;

namespace HW_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //define integer variable a. Read the value of a from console and calculate
            //area and perimetr of square with length a. Output obtained results.
            
            Console.WriteLine("Please enter the length of triangle edge as integer");
            string inputA = Console.ReadLine();
            int a;
            if (int.TryParse(inputA, out a))
            {
                int sqArea = a*a;
                int sqPer = a*4;
                Console.WriteLine("Square area is "+ sqArea);
                Console.WriteLine("Square perimeter is " +sqPer);
            }
            else
            {
                Console.WriteLine("Please enter a number");
            }

            //define string variable name and integer value age. Output question
            //"What is your name?";Read the value name and output next question: 
            //"How old are you,(name)?". Read age and write whole information  

            Console.WriteLine("What is your name?");
            string inputName = Console.ReadLine();
            Console.WriteLine("How old are you, " + inputName + "?");
            string inputAge = Console.ReadLine();
            Console.WriteLine("Your name is " + inputName + ", you are " + 
            inputAge + " years old");

            //Read double number r and calculate the length (l=2*pi*r), area 
            //(S=pi*r*r) and volume (4/3*pi*r*r*r) of a circle of given r 

            double pi = Math.PI;
            
            Console.WriteLine("Please enter square radius as a float");
            string inputRadius = Console.ReadLine();
            double radius;
            if (double.TryParse(inputRadius, out radius))
            {
                double circLength = 2*pi*radius;
                double circArea = pi*radius*radius;
                double circVolume = 4/3*circArea*radius;
                Console.WriteLine("Circle length is "+ circLength);
                Console.WriteLine("Circle are is " + circArea);
                Console.WriteLine("Sphere volume is " + circVolume);
            }
            else
            {
                Console.WriteLine("Please enter a number");
            }
        }
    }
}
