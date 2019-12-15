using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace FractalTreeGenerator
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class GenerateFractalTree : IExternalCommand
    {
        //Hardcoded tree values
        public static double rotAngle = 34;
        public static double lenFactor = 0.78;
        public static double rndCoof = 0.15;
        int treeDepth = 12;

        //setting a randomizer
        public static Random random = new Random();

        //Line list to fill
        static List<Curve> tree = new List<Curve>();

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get Document
            Document doc = uidoc.Document;

            //Use active view for generation
            View view = doc.ActiveView;

            try
            {
                //Pick referenceline
                Reference refLine = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

                //Retrieve Element
                ElementId lineId = refLine.ElementId;
                Element line = doc.GetElement(lineId);
                Curve crv = (line.Location as LocationCurve).Curve;

                if (refLine != null)
                {
                    //call recursive function
                    TreeRecursion(treeDepth, crv);

                    //Place generated lines in Revit
                    using (Transaction trans = new Transaction(doc, "Place Tree"))
                    {
                        trans.Start();

                        foreach (Curve c in tree)
                        {
                            //Wall.Create(doc, c, level.Id, false);
                            doc.Create.NewDetailCurve(view, c);
                        }


                        trans.Commit();
                    }
                }

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

        }
        public static double ToRad(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static int Rnder(double value)
        {
            //generating a random val
            int result = random.Next(Convert.ToInt32(value - (value * rndCoof)), Convert.ToInt32(value + (value * rndCoof)));

            return result;
        }

        public static void TreeRecursion(int depth, Curve branch)
        {
            if (depth > 0)
            {
                //getting angle values
                double radPlus = ToRad(Rnder(rotAngle));
                double radMinus = -ToRad(Rnder(rotAngle));

                //Get start and end points
                XYZ start = branch.GetEndPoint(0);
                XYZ end = branch.GetEndPoint(1);

                //Get length and adjust for branch
                double cLength = branch.Length;
                double lenOne = Rnder(cLength) * lenFactor;
                double lenTwo = Rnder(cLength) * lenFactor;

                //Generating new end points along vector
                XYZ newEndOne = end + (end - start).Normalize().Multiply(lenOne); // * lenOne;
                XYZ newEndTwo = end + (end - start).Normalize().Multiply(lenTwo);

                //XYZ newEndTwo = start + (end - start) * lenTwo;

                //Rotating points to two sides
                newEndOne = new XYZ(
                    Math.Cos(radPlus) * (newEndOne.X - end.X) - Math.Sin(radPlus) * (newEndOne.Y - end.Y) + end.X,
                    Math.Sin(radPlus) * (newEndOne.X - end.X) + Math.Cos(radPlus) * (newEndOne.Y - end.Y) + end.Y,
                    newEndOne.Z);
                Line lineOne = Line.CreateBound(end, newEndOne);
                tree.Add(lineOne);

                //call recursive function
                TreeRecursion(depth - 1, lineOne);

                newEndTwo = new XYZ(
                    Math.Cos(radMinus) * (newEndTwo.X - end.X) - Math.Sin(radMinus) * (newEndTwo.Y - end.Y) + end.X,
                    Math.Sin(radMinus) * (newEndTwo.X - end.X) + Math.Cos(radMinus) * (newEndTwo.Y - end.Y) + end.Y,
                    newEndTwo.Z);
                Line lineTwo = Line.CreateBound(end, newEndTwo);
                tree.Add(lineTwo);

                //call recursive function
                TreeRecursion(depth - 1, lineTwo);
            }
        }
    }
}
