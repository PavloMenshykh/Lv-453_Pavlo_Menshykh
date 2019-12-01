using System;
using System.Collections.Generic;

namespace HW5
{
    class Program
    {
        static void Main(string[] args)
        {
            //task 1

            List<IDeveloper> devs = new List<IDeveloper>();
            Console.WriteLine("how many people you want to write info for ?");
            int count = Convert.ToInt32(Console.ReadLine());
            
            for (int i = 1; i<count+1; i++)
            {
                Console.WriteLine("type 1 to procces person {0} as Programmer, type 2 to process person {0} as Builder", i);
                int type = 0;

                try
                {
                    type = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (type == 1)
                {
                    IDeveloper unID = new Programmer();
                    unID.Create();
                    devs.Add(unID);
                }
                else if (type == 2)
                {
                    IDeveloper unID = new builder();
                    unID.Create();
                    devs.Add(unID);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid type", type);
                }
            }

            devs.Sort();

            foreach (IDeveloper i in devs)
            {
                Console.WriteLine(i);
            }



            //task 2

            Dictionary<int, string> dict = new Dictionary<int, string>();
            int countDict = 7;

            for (int i = 0; i<countDict; i ++)
            {
                Console.WriteLine("Please write your name, person {0}", i + 1);
                string val = Console.ReadLine();
                dict.Add(i+1, val);
            }

            Console.WriteLine("enter id of the person you need, there are {0} people", countDict);

            search:
            try
            {
                int unid = Convert.ToInt32(Console.ReadLine());

                if (unid > countDict)
                {
                    Console.WriteLine("{0} out of range {1}", unid, countDict);
                }
                else
                {
                    Console.WriteLine("{0} person is {1}", unid, dict[unid]);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("not a valid input type");
                goto search;
            }
        }

        public interface IDeveloper: IComparable<IDeveloper>
        {
            string Tool
            { 
                get;
                set;
            }

            new int CompareTo(IDeveloper other);
            void Create();
            void Destroy(List<IDeveloper> devs, IDeveloper dev);
        }

        public class Programmer : IDeveloper
        {
            private string language;

            public string Tool
            {
                get
                {
                    return language;
                }
                set
                {
                    language = value;
                }
            }

            public int CompareTo (IDeveloper other)
            {
                return language.CompareTo(other.Tool);
            }

            public void Create()
            {
                Console.WriteLine("type your language as string");
                language = Console.ReadLine();
            }

            public void Destroy (IList<IDeveloper> devs, IDeveloper dev)
            {
                devs.Remove(dev);
            }

            public void Destroy(List<IDeveloper> devs, IDeveloper dev)
            {
                throw new NotImplementedException();
            }

            public override string ToString()
            {
                return string.Format("Dev uses {0}", language);
            }
        }

        public class builder : IDeveloper
        {
            private string tool;
            public string Tool

            {
                get
                {
                    return tool;
                }
                set
                {
                    tool = value;
                }
            }

            public int CompareTo(IDeveloper other)
            {
                return tool.CompareTo(other.Tool);
            }

            public void Create()
            {
                Console.WriteLine("type your tool as string");
                tool = Console.ReadLine();
            }

            public void Destroy(IList<IDeveloper> devs, IDeveloper dev)
            {
                devs.Remove(dev);
            }

            public void Destroy(List<IDeveloper> devs, IDeveloper dev)
            {
                throw new NotImplementedException();
            }

            public override string ToString()
            {
                return string.Format("Builder uses {0}", tool);
            }
        }
    }
}