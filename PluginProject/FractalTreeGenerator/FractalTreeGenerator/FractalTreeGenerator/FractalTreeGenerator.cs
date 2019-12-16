using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Events;

namespace FractalTreeGenerator
{
    //class to store variables as globals 
    public static class GlobVars
    {
        //Default tree depth
        public static int treeDepth = 7;

        //Hardcoded tree values
        public static double rotAngle = 34;
        public static double lenFactor = 0.78;
        public static double rndCoof = 0.15;

        //setting a randomizer
        public static Random random = new Random();

        //Line list to fill
        public static List<Curve> tree = new List<Curve>();
    }

    //class with UI generations, which calls the main treegeneration class
    public class FractalPanel : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            //Addin tab data
            string RIBBON_PANEL = "Generate fractal tree";

            //Add a tab
            RibbonPanel panel = app.CreateRibbonPanel(RIBBON_PANEL);

            string assemblyName = Assembly.GetExecutingAssembly().Location;

            //add button for command trigger
            PushButtonData buttonData = new PushButtonData(
                "Generate a fractal tree from a line", RIBBON_PANEL, assemblyName,
                "FractalTreeGenerator.GenerateFractalTree");

            PushButton pushButton = panel.AddItem(buttonData) as PushButton;
            pushButton.ToolTip = "Click on a line to turn into a fractal tree";

            //add text input
            TextBoxData itemDepth = new TextBoxData("treeDepth");
            TextBox item1 = panel.AddItem(itemDepth) as TextBox;
            item1.Value = 3;
            item1.ToolTip = "Tree depth (do not use more than 10)";
            item1.EnterPressed += Refresh;

            //refreshes value picked from textbox on enter press
            void Refresh(object sender, TextBoxEnterPressedEventArgs args)
            {
                try
                {
                    TextBox textBoxRefresher = sender as TextBox;
                    GlobVars.treeDepth = Convert.ToInt32(item1.Value.ToString());
                }
                catch
                { 
                }
            }


            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            // nothing to clean up in this simple case
            return Result.Succeeded;
        }
    }


    //main class for tree generation
    [TransactionAttribute(TransactionMode.Manual)]
    public class GenerateFractalTree : IExternalCommand
    {
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

                //If an element was picked run tree generation
                if (refLine != null)
                {
                    //call recursive function
                    TreeRecursion(GlobVars.treeDepth, crv);

                    //Place generated lines in Revit
                    using (Transaction trans = new Transaction(doc, "Place Tree"))
                    {
                        trans.Start();

                        foreach (Curve c in GlobVars.tree)
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


        //Converts degree to radians
        public static double ToRad(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        //generating a random val
        public static int Rnder(double value)
        {
            int result = GlobVars.random.Next(Convert.ToInt32(value - (value * GlobVars.rndCoof)),
                Convert.ToInt32(value + (value * GlobVars.rndCoof)));

            return result;
        }

        //Tree recursive method
        public static void TreeRecursion(int depth, Curve branch)
        {
            if (depth > 0)
            {
                //getting angle values
                double radPlus = ToRad(Rnder(GlobVars.rotAngle));
                double radMinus = -ToRad(Rnder(GlobVars.rotAngle));

                //Get start and end points
                XYZ start = branch.GetEndPoint(0);
                XYZ end = branch.GetEndPoint(1);

                //Get length and adjust for branch
                double cLength = branch.Length;
                double lenOne = Rnder(cLength) * GlobVars.lenFactor;
                double lenTwo = Rnder(cLength) * GlobVars.lenFactor;

                //Generating new end points along vector
                XYZ newEndOne = end + (end - start).Normalize().Multiply(lenOne); 
                XYZ newEndTwo = end + (end - start).Normalize().Multiply(lenTwo);

                //Rotating points to two sides
                newEndOne = new XYZ(
                    Math.Cos(radPlus) * (newEndOne.X - end.X) - Math.Sin(radPlus) * (newEndOne.Y - end.Y) + end.X,
                    Math.Sin(radPlus) * (newEndOne.X - end.X) + Math.Cos(radPlus) * (newEndOne.Y - end.Y) + end.Y,
                    newEndOne.Z);
                Line lineOne = Line.CreateBound(end, newEndOne);
                GlobVars.tree.Add(lineOne);

                //call recursive function
                TreeRecursion(depth - 1, lineOne);

                newEndTwo = new XYZ(
                    Math.Cos(radMinus) * (newEndTwo.X - end.X) - Math.Sin(radMinus) * (newEndTwo.Y - end.Y) + end.X,
                    Math.Sin(radMinus) * (newEndTwo.X - end.X) + Math.Cos(radMinus) * (newEndTwo.Y - end.Y) + end.Y,
                    newEndTwo.Z);
                Line lineTwo = Line.CreateBound(end, newEndTwo);
                GlobVars.tree.Add(lineTwo);

                //call recursive function
                TreeRecursion(depth - 1, lineTwo);
            }
        }
    }
}