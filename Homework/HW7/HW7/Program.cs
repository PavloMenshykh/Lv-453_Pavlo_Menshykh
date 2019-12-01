using System;
using System.Collections.Generic;
using System.IO;

namespace HW7
{
    class Program
    {
        static void Main(string[] args)
        {
            //In Main() method declare Dictionary PhoneBook for keeping pairs PersonName - PhoneNumber. 
            //1) From file "phones.txt" read 9 pairs into PhoneBook.Write only PhoneNumbers into file "Phones.txt".
            //2) Find and print phone number by the given name(name input from console)3) Change all phone numbers, 
            //which are in format 80######### into new format +380#########. The result write into file "New.txt«
            Dictionary<string, int> phoneBook = new Dictionary<string, int>();

            string readPath = @"C:\Users\ilide\Desktop\Lv-453_Pavlo_Menshykh\Homework\HW7\phones.txt";
            string writePath = @"C:\Users\ilide\Desktop\Lv-453_Pavlo_Menshykh\Homework\HW7\onlyphones.txt";
            string writePath38 = @"C:\Users\ilide\Desktop\Lv-453_Pavlo_Menshykh\Homework\HW7\New.txt";
            string line;
            int counter = 1;

            using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
            {
                while((line = sr.ReadLine()) != null)
                {
                    String[] parsing =  line.Split(":", 2, StringSplitOptions.RemoveEmptyEntries);

                    try
                    {
                        int number = Convert.ToInt32("+380"+parsing[1]);
                        int num = Convert.ToInt32(parsing[1]);

                        using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(num);
                        }

                        using (StreamWriter sw = new StreamWriter(writePath38, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(number);
                        }

                        phoneBook.Add(parsing[0], number);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("line {0} not a valid input type", counter);
                    }

                    counter++;
                }
            }

        search:
            Console.WriteLine("enter name of the person you need");
            string name = Console.ReadLine();

            try
            {
                Console.WriteLine(phoneBook[name]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto search;
            }
        }
    }
