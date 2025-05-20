using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System.Diagnostics;

namespace PodDesignPlugin // << This must match the FullClassName used in App.cs
{
    [Transaction(TransactionMode.Manual)]
    public class podDesign : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            PODui window = new PODui();
            window.ShowDialog();
            return Result.Succeeded;
        }

       

    }
}

