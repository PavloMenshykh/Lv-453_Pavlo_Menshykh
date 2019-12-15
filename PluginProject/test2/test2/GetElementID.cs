 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace test2
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class GetElementID : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get UIDocument
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            //Get Document
            Document doc = uidoc.Document;

            try
            {
                //Pick object
                Reference pickedobj = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

                //Retrive Element
                ElementId eleId = pickedobj.ElementId;

                Element ele = doc.GetElement(eleId);

                //Get Element Type
                ElementId eTypeId = ele.GetTypeId();
                ElementType eType = doc.GetElement(eTypeId) as ElementType;

                //Display elememt id
                if (pickedobj != null)
                {
                    //TaskDialog.Show("Element ID", pickedobj.ElementId.ToString());
                    TaskDialog.Show("Element Classification", eleId.ToString() + Environment.NewLine
                        + "Category: " + ele.Category.Name + Environment.NewLine
                        + "Element ID: " + pickedobj.ElementId.ToString() + Environment.NewLine
                        + "Symbol: " + eType.Name);
                }

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
        }
    }
}
