using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.Generic;

namespace PodDesignPlugin
{
    public class PodDesignApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "PNCA® Tools";
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch { /* Tab might already exist, ignore */ }

            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, "POD Designer");

            string dllPath1 = Assembly.GetExecutingAssembly().Location;
            string iconPath1 = Path.Combine(Path.GetDirectoryName(dllPath1), "PodDesignicon.png");

            ImageSource iconImage1 = null;
            if (File.Exists(iconPath1))
            {
                iconImage1 = new BitmapImage(new Uri(iconPath1, UriKind.Absolute));
            }

    
            // POD Type Button
            PushButtonData podBtn = new PushButtonData(
                "POD Button",
                "POD Types",
                dllPath1,
                "PodDesignPlugin.podDesign")
            {
                ToolTip = "Download Standard Types of POD",
                LargeImage = iconImage1
            };

            PushButton podButton = panel1.AddItem(podBtn) as PushButton;


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
