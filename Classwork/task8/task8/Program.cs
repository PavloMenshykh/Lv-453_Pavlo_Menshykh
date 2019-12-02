using System;
using System.Collections.Generic;
using System.IO;

namespace task8
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Add two classes Persons and Staff (use the presentation code)
            //2.Create two classes Teacher and Developer, derived from Staff.
            //Add field subject for class Teacher;
            //Add field level for class Developer;
            //override method Print for both classes.
            //3. In Main, specify a list of Person type and add objects of each
            //type to it. Call for each item in the list method Print ().
            //4. Enter the person's name. If this name present in  list - print 
            //information about this person
            //5. Sort list by name, output to file
            //6. Create a list of Employees and move only workers there. Sort them by salary.

            List<Person> pList = new List<Person>();
            Person p1 = new Teacher("Kim", "theocracy");
            pList.Add(p1);
            Person p2 = new Developer("Carl", "architect");
            pList.Add(p2);

            foreach (Person i in pList)
            {
                Console.WriteLine(i);
            }
            
            bool present = false;

            search:
            Console.WriteLine("Enter persons name to get info");
            string pTest = Console.ReadLine();

            foreach (var i in pList)
            {
                if (i.Name == pTest && present == false)
                {
                    present = true;
                    Console.WriteLine(i);
                }
            }

            if (present == false)
            {
                Console.WriteLine("search again");
                goto search;
            }

            pList.Sort();

            string writePath = @"C:\Users\Pavlo\Desktop\Educational projects\Lv-453_Pavlo_Menshykh\Classwork\task8\output.txt";

            foreach (Person i in pList)
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(i.ToString());
                }
            }

            Console.ReadLine();
        }

        abstract class Person:IComparable<Person>
        {
            public string Name
            {
                get;
                set;
            }

            public Person(string name)
            {
                Name = name;
            }

            public int CompareTo(Person other)
            {
                return Name.CompareTo(other.Name);
            }
        }

        abstract class Staff : Person
        {
            public Staff(string name) : base(name)
            {
                Name = name;
            }
        }

        class Teacher : Staff
        {
            public string Subject { get; set; }

            public Teacher(string name, string subject) : base(name)
            {
                Subject = subject;
            }

            public override string ToString()
            {
                return string.Format("Teachers name is {0}, subject is {1}", Name, Subject);
            }
        }

        class Developer : Staff
        {
            public string Level { get; set; }
            public Developer(string name, string level) : base(name)
            {
                Level = level;
            }
            public override string ToString()
            {
                return string.Format("Developers name is {0}, level is {1}", Name, Level);
            }
        }
    }
}
