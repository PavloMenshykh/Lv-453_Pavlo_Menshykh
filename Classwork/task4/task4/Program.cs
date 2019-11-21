using System;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Define class Car with fields name, color, price and const field CompanyName          	
            //Create two constructors - default and with parameters.
            //Create a property to access the color field.                                           			
            //Define methods: Input () - to enter car data from the console,				
            //Print () - to output the machine data to the console					
            //ChangePrice (double x) - to change the price by x%
            //Enter data about 3 cars.
            //Decrease their price by 10 %, display info about the car.
            //Enter a new color and paint the car with the color white in the specified color
            //Overload the operator == for the class Car (cars - equal if the name and price are equal)
            //Overload the method ToString()  in the class Car, which returns a line with data about the car

            Car first = new Car();
            Car second = new Car();
            Car third = new Car();

            Console.WriteLine("Enter data about cars");

            first.Input();
            //second.Input();
            //third.Input();

            Console.WriteLine("Enter discount percentage as integer");
            int perc = Convert.ToInt32(Console.ReadLine());
            first.ChangePrice(perc);

            Console.WriteLine(first);
            //Console.WriteLine(second);
            //Console.WriteLine(third);
        }

        public class Car
        {
            public string name;
            private string color;
            public double price;
            private const string companyName = "Lv-453";

            public Car()
            {

            }

            public Car(string name, string color, double price)
            {
                this.name = name;
                this.color = color;
                this.price = price;
            }

            public string Color
            {
                get
                {
                    return color;
                }

                set
                {

                    color = value;
                }
            }

            public void Input()
            {
                Console.WriteLine("Please enter car name");
                name = Console.ReadLine();
                Console.WriteLine("Please enter car color");
                color = Console.ReadLine();
                Console.WriteLine("Please enter car price");
                price = Convert.ToDouble(Console.ReadLine());
            }

            public string Print()
            {
                return "Car name is " + name + ", car color is " + color + ", car price is " + price;
            }

            public void ChangePrice(double x)
            {
                price -= price * x / 100;
            }

            public override string ToString()
            {
                return string.Format("This cars name is {0}, it is {1} and costs {2} from {3}", name, color, price, companyName);
            }

            public static bool operator == (Car one, Car two)
            {
                return (one.name == two.name && one.price == two.price);
            }

            public static bool operator != (Car one, Car two)
            {
                return !(one == two);
            }
        }
    }
}
