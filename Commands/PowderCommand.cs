using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace PodDesignPlugin // << This must match the FullClassName used in App.cs
{
    [Transaction(TransactionMode.Manual)]
    public class PowderCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            PowderWindow window = new PowderWindow();
            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}

