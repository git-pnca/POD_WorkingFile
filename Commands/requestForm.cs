using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace PodDesignPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class requestForm : IExternalCommand
    {
        private const string RequestUrl = "https://forms.office.com/pages/responsepage.aspx?id=cPqvSIyCOkqMhTgWY7dGO2WxN0EKtuFPhhuTHK-Hc4dUMjNWTVVBUTBCQUk4TFI5Mjk3S1JFOVpYNy4u&route=shorturl";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Open the request form URL in the default web browser
                System.Diagnostics.Process.Start(RequestUrl);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", "Failed to open the request form: " + ex.Message);
                return Result.Failed;



            }

            return Result.Succeeded;
        }

        
    }
}
