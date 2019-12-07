using System;
using System.Collections.Generic;
using System.Linq;

namespace HW8
{
    class Program
    {
        static void Main(string[] args)
        {
            //1) Create abstract class Shape with field name and property Name. 
            //Add constructor with 1 parameter and abstract methods Area() and
            //Perimeter(), which can return area and perimeter of shape; 
            //Create classes Circle, Square derived from Shape with field 
            //radius(for Circle) and side(for Square).   Add necessary constructors, 
            //properties to these classes, override methods from abstract class Shape. 
            //a) In Main() create list of Shape, then ask user to enter data of 10 
            //different shapes.Write name, area and perimeter of all shapes. 
            //b) Find shape with the largest perimeter and print its name.
            //3) Sort shapes by area and print obtained list(Remember about IComparable)

            int shapeCount = 3;
            List<Shape> shapes = new List<Shape>();

            for (int i = 0; i<shapeCount; i++)
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

            var lPer = shapes.OrderByDescending(i => i.perimeter);
            Console.WriteLine("Shape with largest perimeter is {0}", lPer.ElementAt(0).Name);

            Console.WriteLine("Shapes sorted by Area");
            var sorted = shapes.OrderBy(i => i.area);
            foreach (var i in sorted)
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }

        abstract class Shape //: IComparable<Shape>
        {
            public string Name
            {
                get;
                set;
            }
            public Shape (string name)
            {
                Name = name;
            }

            public abstract double Area();
            public abstract double Perimeter();

            public double perimeter { get => Perimeter(); }
            public double area { get => Area(); }

            //public int CompareTo(Shape other)
            //{
            //    return Area().CompareTo(other.Area());
            //}
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
