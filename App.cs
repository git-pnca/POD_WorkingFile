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
            string tabName = "PNCA®";
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch { /* Tab might already exist, ignore */ }

            RibbonPanel panel1 = application.CreateRibbonPanel(tabName, "POD Design");

            string dllPath1 = Assembly.GetExecutingAssembly().Location;
            string iconPath1 = Path.Combine(Path.GetDirectoryName(dllPath1), "PodDesignicon.png");

            ImageSource iconImage1 = null;
            if (File.Exists(iconPath1))
            {
                iconImage1 = new BitmapImage(new Uri(iconPath1, UriKind.Absolute));
            }

            // Create SplitButton
            SplitButtonData splitData1 = new SplitButtonData("PODDesigns", "POD Designs");
            SplitButton split1 = panel1.AddItem(splitData1) as SplitButton;

            // Shower Button
            PushButtonData showerBtnData1 = new PushButtonData(
                "ShowerButton",
                "Shower Type",
                dllPath1,
                "PodDesignPlugin.ShowerCommand")
            {
                ToolTip = "Download Standard Shower Type Pod",
                LargeImage = iconImage1
            };
            split1.AddPushButton(showerBtnData1);

            // Powder Button
            PushButtonData powderBtnData1 = new PushButtonData(
                "PowderButton",
                "Powder Type",
                dllPath1,
                "PodDesignPlugin.PowderCommand")
            {
                ToolTip = "Download Standard Powder Type Pod",
                LargeImage = iconImage1
            };
            split1.AddPushButton(powderBtnData1);

            // Maid Sq Button
            PushButtonData maidsqBtnData1 = new PushButtonData(
                "MaidSqButton",
                "MaidSq Type",
                dllPath1,
                "PodDesignPlugin.MaidSqCommand")
            {
                ToolTip = "Download Standard Square Maid Type Pod",
                LargeImage = iconImage1
            };
            split1.AddPushButton(maidsqBtnData1);


            // Maid L Button
            PushButtonData maidlBtnData1 = new PushButtonData(
                "MaidLButton",
                "MaidL Type",
                dllPath1,
                "PodDesignPlugin.MaidLCommand")
            {
                ToolTip = "Download Standard 'L' Maid Type Pod",
                LargeImage = iconImage1
            };
            split1.AddPushButton(maidlBtnData1);

            // Request for Custom Pod Panel

            RibbonPanel panel2 = application.CreateRibbonPanel(tabName, "Request for custom Pod");

            string dllPath2 = Assembly.GetExecutingAssembly().Location;
            string iconPath2 = Path.Combine(Path.GetDirectoryName(dllPath2), "Requesticon.png");

            ImageSource iconImage2 = null;

            if (File.Exists(iconPath2))
            {
                iconImage2 = new BitmapImage(new Uri(iconPath2, UriKind.Absolute));
            }

            // Create SplitButton
            SplitButtonData splitData2 = new SplitButtonData("CustomPod", "Request for Custom Pod");
            SplitButton split2 = panel2.AddItem(splitData2) as SplitButton;

            // Shower Button
            PushButtonData showerBtnData2 = new PushButtonData(
                "ShowerButton",
                "Shower",
                dllPath2,
                "PodDesignPlugin.ShowerQueryCommand")
            {
                ToolTip = "Request for Custom Shower Pod",
                LargeImage = iconImage2
            };
            split2.AddPushButton(showerBtnData2);

            // Powder Button
            PushButtonData powderBtnData2 = new PushButtonData(
                "PowderButton",
                "Powder",
                dllPath2,
                "PodDesignPlugin.PowderQueryCommand")
            {
                ToolTip = "Request for Custom Powder Pod",
                LargeImage = iconImage2
            };
            split2.AddPushButton(powderBtnData2);

            // Maid Sq Button
            PushButtonData maidsqBtnData2 = new PushButtonData(
                "MaidSqButton",
                "MaidSq",
                dllPath2,
                "PodDesignPlugin.MaidSqQueryCommand")
            {
                ToolTip = "Request for Custom Square Maid Pod",
                LargeImage = iconImage2
            };
            split2.AddPushButton(maidsqBtnData2);

            // Maid L Button
            PushButtonData maidlBtnData2 = new PushButtonData(
                "MaidLButton",
                "MaidL",
                dllPath2,
                "PodDesignPlugin.MaidLQueryCommand")
            {
                ToolTip = "Request for Custom 'L' Maid Pod",
                LargeImage = iconImage2
            };
            split2.AddPushButton(maidlBtnData2);

            //IList<RibbonItem> stackedItems = ribbonPanel.AddStackedItems(split1, split2);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
