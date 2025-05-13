using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace PodDesignPlugin // << This must match the FullClassName used in App.cs
{
    [Transaction(TransactionMode.Manual)]
    public class MaidSqCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MaidSqWindow window = new MaidSqWindow();
            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}