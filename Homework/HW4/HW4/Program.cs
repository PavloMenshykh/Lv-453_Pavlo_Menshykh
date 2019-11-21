using System;

namespace HW4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("To do person creation");
        }

        public class Person
        {
            private string name;
            private DateTime birthYear;


            public string Name
            {
                get
                {
                    return name;
                }
            }

            public DateTime BirthYear
            {
                get
                {
                    return birthYear;
                }
            }

            public Person(string name, DateTime birthYear)
            {
                this.name = name;
                this.birthYear = birthYear;
            }

            public int Age()
            {
                int bYear = BirthYear.Year;
                int nYear = DateTime.Now.Year;
                int age = nYear - bYear;
                return age; 
            }

            public void Input()
            {
                Console.WriteLine("Please write name");
                name = Console.ReadLine();
                Console.WriteLine("Please enter your bith date, for example xxxx-xx-xx");
                string dateData = Console.ReadLine();
                birthYear = DateTime.Parse(dateData); 
            }

            public void ChangeName(string i)
            {
                name = i;
            }

            public override string ToString()
            {
                return string.Format("Persons name is {0}, birthyear is {1}", name, birthYear);
            }

            public void Output()
            {
                Console.WriteLine(ToString());
            }

            public static bool operator == (Person one, Person two)
            {
                return one.name == two.name;
            }
            public static bool operator != (Person one, Person two)
            {
                return !(one == two);
            }
        }
    }
}
