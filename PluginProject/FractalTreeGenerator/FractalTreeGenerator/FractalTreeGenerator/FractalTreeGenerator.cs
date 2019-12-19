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
    public class GlobVars
    {
        //Default tree depth
        public static int treeDepth = 7;

        //Hardcoded tree values
        public static double rotAngle = 28;
        public static double lenFactor = 0.78;
        public static double lenFactorChristmas = lenFactor / 2;
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
                    //ChristmasRecursion(GlobVars.treeDepth, crv, GlobVars.tree, GlobVars.rotAngle, GlobVars.lenFactorChristmas);
                    TreeRecursion(GlobVars.treeDepth, crv, GlobVars.tree, GlobVars.rotAngle, GlobVars.lenFactor);

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
        public double ToRad(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        //generating a random val
        public int AddRandom(double value)
        {
            int result = GlobVars.random.Next(Convert.ToInt32(value - (value * GlobVars.rndCoof)),
                Convert.ToInt32(value + (value * GlobVars.rndCoof)));

            return result;
        }

        //get line end moved along it's axis
        public XYZ MoveAlongLine(double distance, Curve line)
        {
            //Get start and end points
            XYZ start = line.GetEndPoint(0);
            XYZ end = line.GetEndPoint(1);

            //Move line end point by distance
            XYZ result = end + (end - start).Normalize().Multiply(distance);
            return result;
        }

        //rotate a point along a reference point
        public XYZ RotatePoint(double angle, XYZ reference, XYZ toRotate)
        {
            //mathematical rotation in 2 dimentions
            XYZ result = new XYZ(
                Math.Cos(angle) * (toRotate.X - reference.X) - Math.Sin(angle) * (toRotate.Y - reference.Y) + reference.X,
                Math.Sin(angle) * (toRotate.X - reference.X) + Math.Cos(angle) * (toRotate.Y - reference.Y) + reference.Y,
                toRotate.Z);

            return result;
        }

        //generate tree side branch
        public Curve SideBranch(double angle, Curve branch, double scaleFactor)
        {
            //Get length and adjust for branch
            double cLength = branch.Length;

            //Get end point of branch
            XYZ end = branch.GetEndPoint(1);

            //generate move distance
            double lengthRnd = AddRandom(cLength) * scaleFactor;

            //generate new branch
            XYZ pntMoved = MoveAlongLine(lengthRnd, branch);
            XYZ pntRotated = RotatePoint(angle, end, pntMoved);
            Line generatedBranch = Line.CreateBound(end, pntRotated);

            return generatedBranch;
        }

        //generate both side branches
        public void GenerateSideBranches(int depth, Curve branch, List<Curve> listToFill, double rotationAngle, double scaleFactor,
            Action<int, Curve, List<Curve>, double, double> method)
        {
            for (int i = 0; i < 2; i++)
            {
                //getting angle values
                double rotation = ToRad(AddRandom(rotationAngle));

                //generate a + and - rotations
                if (i == 1) rotation = -rotation;

                Curve newBranch = SideBranch(rotation, branch, scaleFactor);
                listToFill.Add(newBranch);

                //call next recursion
                method(depth - 1, newBranch, listToFill, rotationAngle, scaleFactor);
            }
        }


        //Tree recursive method
        public void TreeRecursion(int depth, Curve branch, List<Curve> listToFill, double rotationAngle, double scaleFactor)
        {
            if (depth > 0)
            {
                //test for short length
                try
                {
                    branch.GetEndPoint(1);

                    //asssign recursion type
                    Action<int, Curve, List<Curve>, double, double> recursionType = TreeRecursion;

                    GenerateSideBranches(depth, branch, listToFill, rotationAngle, scaleFactor, recursionType);
                }
                catch
                {
                    //simply return nothing
                }
            }
        }


        //Christmas tree recursive method
        public void ChristmasRecursion(int depth, Curve branch, List<Curve> listToFill, double rotationAngle, double scaleFactor)
        {
            if (depth > 0)
            {
                try
                {
                    //Get end point of branch
                    XYZ end = branch.GetEndPoint(1);

                    //asssign recursion type
                    Action<int, Curve, List<Curve>, double, double> recursionType = ChristmasRecursion;

                    //additional branch for christmas tree
                    //Get length and adjust for branch
                    double cLength = branch.Length;

                    //generate move distance
                    double lengthRnd = AddRandom(cLength) * GlobVars.lenFactor;

                    XYZ newPoint = MoveAlongLine(lengthRnd, branch);
                    Curve newBranch = Line.CreateBound(end, newPoint);
                    listToFill.Add(newBranch);

                    //call next recursion
                    ChristmasRecursion(depth - 1, newBranch, listToFill, rotationAngle, scaleFactor);
                    GenerateSideBranches(depth, branch, listToFill, rotationAngle, scaleFactor, recursionType);
                }
                catch
                { 
                    //simply return nothing
                }
            }
        }
    }
}