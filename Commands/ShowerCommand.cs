using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace PodDesignPlugin // << This must match the FullClassName used in App.cs
{
    [Transaction(TransactionMode.Manual)]
    public class ShowerCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            ShowerWindow window = new ShowerWindow();
            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}

