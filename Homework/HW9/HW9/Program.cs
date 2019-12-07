using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HW9
{
    class Program
    {
        static void Main(string[] args)
        {
            int shapeCount = 3;
            List<Shape> shapes = new List<Shape>();

            for (int i = 0; i < shapeCount; i++)
            {
                Console.WriteLine("Write 'S' if you want to define a square, and 'C' if you want to define a circle");
                string shapeTest = Console.ReadLine();
                if (shapeTest == "S")
                {

                    Console.WriteLine("Please enter name");
                    string nm = Console.ReadLine();
                    int sd = 0;
                totest:
                    Console.WriteLine("Please enter side");
                    try
                    {
                        sd = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter value as integer");
                        goto totest;
                    }
                    Console.WriteLine("Data recorded");
                    Shape sq = new Square(nm, sd);
                    shapes.Add(sq);
                }
                else if (shapeTest == "C")
                {

                    Console.WriteLine("Please enter name");
                    string nm = Console.ReadLine();
                    int rd = 0;
                totest:
                    Console.WriteLine("Please enter radius");
                    try
                    {
                        rd = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter value as integer");
                        goto totest;
                    }
                    Console.WriteLine("Data recorded");
                    Shape cr = new Circle(nm, rd);
                    shapes.Add(cr);
                }
                else
                {
                    Console.WriteLine("You have not entered a correct shape ID");
                }
            }

            //2) Find and write into the file shapes with area from range[10, 100]

            int rangeLower = 10;
            int rangeHigher = 100;

            var inRange = from i in shapes
                          where i.area > rangeLower && i.area < rangeHigher
                          select i;

            Console.WriteLine("Shapes with are larger than {0} and smaller than {1} are:", rangeLower, rangeHigher);
            foreach (var i in inRange)
            {
                Console.WriteLine(i);
            }

            //3) Find and write into the file shapes which name contains letter 'a'

            char checker = 'a';
            Console.WriteLine("Shapes whos name contains letter {0} are:", checker);

            var withChar = from i in shapes
                           where i.Name.Contains(checker)
                           select i;

            foreach (var i in withChar)
            {
                Console.WriteLine(i);
            }

            //4) Find and remove from the list all shapes with perimeter less then 5. Write resulted list into Console 
            int perTest = 5;
            Console.WriteLine("Shapes whos perimeter is larger than {0} are:", perTest);

            shapes.RemoveAll(i => i.perimeter < perTest);

            foreach (Shape i in shapes)
            {
                Console.WriteLine(i);
            }


            Console.WriteLine("");
            Console.WriteLine("!=======================");
            Console.WriteLine("Part two");
            Console.WriteLine("!=======================");
            Console.WriteLine("");

            //Prepare txt file with a lot of text inside (for example take you .cs file from previos homework)
            //Read all lines of text from file into array of strings.
            //Each array item contains one line from file.
            //Complete next tasks:
            //1) Count and write the number of symbols in every line.
            //2) Find the longest and the shortest line.
            //3) Find and write only lines, which consist of word "var"

            string checker2 = "var";

            string readPath = @"C:\Users\ilide\Desktop\Lv-453_Pavlo_Menshykh\Homework\HW8\HW8\Program.cs";
            List<string> reader = new List<string>();
            List<int> readerlen = new List<int>();
            string line;

            using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    reader.Add(line);

                    int symbCount = line.Length;
                    readerlen.Add(symbCount);
                    Console.WriteLine("Number of symbols is {0}", symbCount);
                }
            }

            var minlen = readerlen.Min();
            var maxlen = readerlen.Max();
            Console.WriteLine("Longest string has {1} symbols, shortest has {0}", minlen, maxlen);

            var withVar = from i in reader
                          where i.Contains(checker2)
                          select i;

            Console.WriteLine("String with {0} in them are:", checker2);

            foreach (var i in withVar)
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }

        abstract class Shape
        {
            public string Name
            {
                get;
                set;
            }
            public Shape(string name)
            {
                Name = name;
            }

            public abstract double Area();
            public abstract double Perimeter();

            public double perimeter { get => Perimeter(); }
            public double area { get => Area(); }
        }

        class Square : Shape
        {
            public int Side { get; set; }

            public Square(string name, int side) : base(name)
            {
                Side = side;
            }

            public override double Area()
            {
                return Side * Side;
            }

            public override double Perimeter()
            {
                return 4 * Side;
            }

            public override string ToString()
            {
                var area = Area();
                var perimeter = Perimeter();
                return string.Format("Square area is {0}, perimeter is {1}", area, perimeter);
            }
        }


        class Circle : Shape
        {
            public int Radius { get; set; }
            public Circle(string name, int radius) : base(name)
            {
                Radius = radius;
            }

            public override double Area()
            {
                return Math.PI * Radius * Radius;
            }

            public override double Perimeter()
            {
                return 2 * Math.PI * Radius;
            }

            public override string ToString()
            {
                var area = Area();
                var perimeter = Perimeter();
                return string.Format("Circle area is {0}, perimeter is {1}", area, perimeter);
            }
        }
    }
}