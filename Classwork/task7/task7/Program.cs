using System;
using System.Collections.Generic;
using System.IO;

namespace task7
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "";
            string readPath = @"C:\Users\Pavlo\Desktop\Educational projects\Lv-453_Pavlo_Menshykh\Classwork\task7\data.txt";
            string writePath = @"C:\Users\Pavlo\Desktop\Educational projects\Lv-453_Pavlo_Menshykh\Classwork\task7\rez.txt";

            try
            {
                using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                {
                    text = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string readC = @"C:\Users\Pavlo\Desktop\Educational projects\Lv-453_Pavlo_Menshykh\Classwork\task7\DirectoryC.txt";
            string[] dirs;
            string dirName = "C:\\";

            try
            {
                dirs = Directory.GetDirectories(dirName);
                foreach (string d in dirs)
                {
                    using (StreamWriter sw = new StreamWriter(readC, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(d);
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string folderCW = @"C:\Users\Pavlo\Desktop\Educational projects\Lv-453_Pavlo_Menshykh\Classwork\task7";
            List<string> textData = new List<string>();
            string[] files;

            files = Directory.GetFiles(folderCW);

            foreach (string f in files)
            {
                FileInfo info = new FileInfo(f);
                if (info.Extension == ".txt")
                {
                    string t = "";
                    using (StreamReader sr = new StreamReader(f, System.Text.Encoding.Default))
                    {
                        t = sr.ReadToEnd();
                    }
                    textData.Add(t);
                }
            }

            foreach (string i in textData)
            {
                Console.WriteLine(i);
            }

        }
    }
}
