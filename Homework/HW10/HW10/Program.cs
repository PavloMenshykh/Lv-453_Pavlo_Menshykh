using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student("Carl");
            Parent parentOne = new Parent();
            Accountancy accountant = new Accountancy();

            student.MarkChange += parentOne.OnMarkChange;
            student.MarkChange += accountant.PayingFellowship;

            Random rndMarks = new Random();

            int markscount = 10;

            for (int i = 0; i > markscount; i++)
            {
                int rndMark = rndMarks.Next(50, 100);
                student.AddMark(rndMark);
            }

            Console.ReadLine();
        }
    }

    public delegate void MyDel(int m);
    public class Student
    {
        string name;

        public Student (string name)
        {
            this.name = name;
        }

        public static  List<int> marks = new List<int>();
        public event MyDel MarkChange;

        public void AddMark(int newMark)
        {
            marks.Add(newMark);
            if (MarkChange != null)
            {
                MarkChange.Invoke(newMark);
            }
        }
    }

    public class Parent
    {
        public void OnMarkChange(int estimateMark)
        {
            Console.WriteLine("Mark estimate is {0}", estimateMark);
        }
    }

    public class Accountancy
    {
        //hardcoded scholarship level
        int checker = 80;

        public List<int> marks = new List<int>();
        public void PayingFellowship(int mark)
        {
            marks.Add(mark);
            Console.WriteLine(marks.Sum() > checker ? 
                "You will get scholarship" : "No scholarship for you");
        }
    }
}
