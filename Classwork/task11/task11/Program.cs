using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace task11
{
    public class Program
    {
        static void Main(string[] args)
        {
            //create class
            Square sq = new Square();
            sq._id = 1;
            sq._name = "squr";
            sq._side = 50;

            //binary
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("Square.bin",
            FileMode.Create, FileAccess.Write, FileShare.None);

            formatter.Serialize(stream, sq);
            stream.Close();


            //xml
            XmlSerializer xmlser = new XmlSerializer(typeof(Square));
            Stream serialStream = new FileStream("Square.xml", FileMode.Create);

            xmlser.Serialize(serialStream, sq);

            //json
            Stream file = new FileStream("square.json", FileMode.Create);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Square));

            ser.WriteObject(file, sq);
        }

        [Serializable]
        public abstract class Shape
        {
            [XmlAttribute]
            public string Name
            {
                get;
                set;
            }


            public Shape() { }

            public abstract double Area();
            public abstract double Perimeter();

            [XmlAttribute]
            public double perimeter { get => Perimeter(); }

            [XmlAttribute]
            public double area { get => Area(); }
        }

        [Serializable]
        public class Square : Shape
        {

            [XmlAttribute]
            public int _id;

            [XmlAttribute]
            public int _side;

            [XmlAttribute]
            public string _name;

            [XmlAttribute]
            public int Side { get; set; }

            public Square() { }

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
        
    }
}
