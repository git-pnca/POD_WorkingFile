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

            // For First Button - POD Designer

            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, "POD Downloader");

            string dllPath1 = Assembly.GetExecutingAssembly().Location;
            string iconPath1 = Path.Combine(Path.GetDirectoryName(dllPath1), "modular20Icon.png");
            string iconPath2 = Path.Combine(Path.GetDirectoryName(dllPath1), "req20Icon.png");
            string iconPath3 = Path.Combine(Path.GetDirectoryName(dllPath1), "view20Icon.png");



            // For second button - Views Manager

            RibbonPanel panel2 = application.CreateRibbonPanel(tabName, "View Manager");





            ImageSource iconImage1 = null;
            if (File.Exists(iconPath1))
            {
                iconImage1 = new BitmapImage(new Uri(iconPath1, UriKind.Absolute));
            }

            ImageSource iconImage2 = null;
            if (File.Exists(iconPath2))
            {
                iconImage2 = new BitmapImage(new Uri(iconPath2, UriKind.Absolute));
            }

            ImageSource iconImage3 = null;
            if (File.Exists(iconPath3))
            {
                iconImage3 = new BitmapImage(new Uri(iconPath3, UriKind.Absolute));
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


            PushButtonData requestBtn = new PushButtonData(
                "Request Button",
                "Request Form",
                dllPath1,
                "PodDesignPlugin.requestForm")
            {
                ToolTip = "Request a New Custom POD Type",
                LargeImage = iconImage2
            };

            PushButton requestButton = panel1.AddItem(requestBtn) as PushButton;

            PushButtonData viewBtn = new PushButtonData(
                "View Button",
                "Views Creation",
                dllPath1,
                "PodDesignPlugin.ViewsCreation")
            {
                ToolTip = "Create Views for Model as per the disciplines",
                LargeImage = iconImage3
            };

            PushButton vBtn = panel2.AddItem(viewBtn) as PushButton;


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
