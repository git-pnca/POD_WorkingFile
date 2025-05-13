using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodDesignPlugin
{
    [Transaction(TransactionMode.Manual)]
    public class PowderQueryCommand : IExternalCommand
    {
        // URL to open (you can change it)
        private readonly string targetUrl = "https://forms.office.com/pages/responsepage.aspx?id=cPqvSIyCOkqMhTgWY7dGO2WxN0EKtuFPhhuTHK-Hc4dUMVFCR1haNExXSk1MRVRDQksxVkQ1TFZNNC4u&route=shorturl";

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = targetUrl,
                    UseShellExecute = true
                });

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = $"Failed to open URL: {ex.Message}";
                return Result.Failed;
            }
        }
    }
}
