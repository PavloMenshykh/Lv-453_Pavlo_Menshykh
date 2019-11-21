using System;
using System.Collections.Generic;

namespace task5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Develop interface IFlyable with method Fly().
            //Create two classes Bird(with fields: name and canFly) 
            //and Plane(with fields: mark and highFly) , which 
            //implement interface IFlyable.
            //Create List of IFlyable objects and add some Birds 
            //and Planes to it.Call Fly() method for every item from 
            //the list of it.

            List<IFlyable> flyObjects = new List<IFlyable>();
            flyObjects.Add(new Bird(name:"colibri", canFly:true));
            flyObjects.Add(new Plane(mark:"jet", highFly: 1000));

            foreach (IFlyable i in flyObjects)
            {
                i.Fly();
            }

            //Declare myColl of 10 integers and fill it from Console.
            //1) Find and print all positions of element - 10 in the collection
            //2) Remove from collection elements, which are greater then 20.Print collection
            //3) Insert elements 1,-3,-4 in positions 2, 8, 5.Print collection
            //4) Sort and print collection
            //Use next Collections for this tasks: List or ArrayList

            List<int> myColl = new List<int>();

            int numOfElems = 10;

            for (int i = 1; i < 1+numOfElems; i++)
            {
                Console.WriteLine("Please input integer {0}", i);
                myColl.Add(Convert.ToInt32(Console.ReadLine()));
            }

            Console.WriteLine("===============================");

            int count = 0;
            foreach (int p in myColl)
            {
                if (p == -10)
                {
                    Console.WriteLine(count);
                }
                count++;
            }

            Console.WriteLine("===============================");

            foreach (int l in myColl)
            {
                Console.WriteLine(l);
            }

            Console.WriteLine("===============================");

            for (int k = 0; k<numOfElems; k++)
            {
                if (myColl[k] == 20)
                {
                    myColl.RemoveAt(k);
                    k--;
                    numOfElems--;
                }   
            }

            foreach (int l in myColl)
            {
                Console.WriteLine(l);
            }

            Console.WriteLine("===============================");

            myColl.Insert(2, 1);
            myColl.Insert(5, -4);

            foreach (int l in myColl)
            {
                Console.WriteLine(l);
            }

            Console.WriteLine("===============================");

            myColl.Sort();

            foreach (int l in myColl)
            {
                Console.WriteLine(l);
            }

            Console.ReadKey();
        }

        interface IFlyable
        {
            void Fly();
        }

        class Bird : IFlyable
        {
            private string name;
            private bool canFly;

            public Bird(string name, bool canFly)
            {
                this.name = name;
                this.canFly = canFly;
            }
            public override string ToString()
            {
                return string.Format("Birds name is {0}, bird can fly ? {1}", name, canFly);
            }

            public void Fly()
            {
                Console.WriteLine(ToString());
            }
        }

        class Plane : IFlyable
        {
            private string mark;
            private int highFly;

            public Plane(string mark, int highFly)
            {
                this.mark = mark;
                this.highFly = highFly;
            }
            public override string ToString()
            {
                return string.Format("Plane mark is {0}, flyheight is {1}", mark, highFly);
            }

            public void Fly()
            {
                Console.WriteLine(ToString());
            }
        }

    }
}

